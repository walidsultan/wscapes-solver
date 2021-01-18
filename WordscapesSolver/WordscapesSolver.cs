using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using WS.Wscapes.DataTypes;

namespace WS.Wscapes
{
    public partial class WordscapesSolver : Form
    {
        private AppiumDriver<AndroidElement> _driver;
        private static System.Timers.Timer _screenshotTimer;
        private static System.Timers.Timer _stateTimer;
        private static System.Timers.Timer _actionTimer;
        private OCR _ocr;

        private CharComparer _charComparer;
        private static object _originalScreenShotLock = new object();

        private const int SCREENSHOT_TIMER_CONSTANT = 500;
        private const int STATE_TIMER_CONSTANT = 100;
        private const int ACTION_TIMER_CONSTANT = 100;

        private const int IMPLICIT_WAIT_TIME = 2000;

        private const string START_TOUCH_SCRIPT = "su -c 'sendevent /dev/input/event2 3 57 13;";
        private const string POSITION_SCRIPT = "sendevent /dev/input/event2 3 53 {0};sendevent /dev/input/event2 3 54 {1};sendevent /dev/input/event2 3 58 17;sendevent /dev/input/event2 0 0 0;";
        private const string END_TOUCH_SCRIPT = "sendevent /dev/input/event2 3 57 -1;sendevent /dev/input/event2 0 0 0;'";

        private const string WORDS_SERVICE_URL = "https://wscapes-solver.azurewebsites.net/solve?letters={0}";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private const int OCR_MATHCING_CONTINUE_WORD_LEFT = 440;
        private const int OCR_MATHCING_CONTINUE_WORD_TOP = 2020;
        private const int OCR_MATHCING_CONTINUE_WORD_WIDTH = 590;
        private const int OCR_MATHCING_CONTINUE_WORD_HEIGHT = 70;

        private SwipeMethod _swipeMethod;
        private int _nativeSwipePausePerLetter = 1;

        private int _equalCharactersSequenceCount = 0;
        public WordscapesSolver()
        {
            InitializeComponent();

            _ocr = new OCR();
            _charComparer = new CharComparer();
            _swipeMethod = SwipeMethod.Native;
            txtNativeSwipePause.Text = _nativeSwipePausePerLetter.ToString();

            NotifyIcon ni = new NotifyIcon();
            ni.Icon = new Icon("App_Data/icon.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                };

        }

        private void WordscapesSolver_Load(object sender, EventArgs e)
        {
            var swipeMethods = Enum.GetValues(typeof(SwipeMethod)).Cast<SwipeMethod>().Select(x => x.ToString()).ToArray();
            cbSwipeMethod.Items.AddRange(swipeMethods);
            cbSwipeMethod.SelectedItem = _swipeMethod.ToString();
        }

        private void SetTimers()
        {
            _screenshotTimer = new System.Timers.Timer(SCREENSHOT_TIMER_CONSTANT);
            _screenshotTimer.Elapsed += _screenshotTimer_Elapsed;
            _screenshotTimer.AutoReset = true;
            _screenshotTimer.Enabled = true;

            _stateTimer = new System.Timers.Timer(STATE_TIMER_CONSTANT);
            _stateTimer.Elapsed += _stateTimer_Elapsed;
            _stateTimer.AutoReset = true;
            _stateTimer.Enabled = true;

            _actionTimer = new System.Timers.Timer(ACTION_TIMER_CONSTANT);
            _actionTimer.Elapsed += _actionTimer_Elapsed;
            _actionTimer.AutoReset = true;
            _actionTimer.Enabled = true;
        }

        private async void _actionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _actionTimer.Enabled = false;

            switch (AppState.CurrentGameState)
            {
                case GameState.Menu:
                case GameState.LevelSolved:
                case GameState.PiggyBank:
                case GameState.ReOrderLevel:
                    TapScreen();
                    break;
                case GameState.Puzzle:
                    if (AppState.PreviousLevelControls != null && AppState.PreviousLevelControls.SequenceEqual(AppState.CurrentLevelControls, _charComparer))
                    {

                        if (_equalCharactersSequenceCount >= 1)
                        {
                            log.Info("Set re-order state. Equal Sequence.");
                            SetReorderState();
                            break;
                        }
                        else
                        {
                            _equalCharactersSequenceCount++;
                        }
                    }
                    else
                    {
                        _equalCharactersSequenceCount = 0;
                    }

                    AppState.PreviousLevelControls = AppState.CurrentLevelControls.ToList();
                    // log.Info($"Start solve level. Controls: {new string(AppState.CurrentLevelControls.Select(x => x.Char).ToArray())}");
                    await SolveLevel(AppState.CurrentLevelControls);
                    break;
            }
            _actionTimer.Enabled = true;

        }

        private void SetReorderState()
        {
            //Change characters order
            AppState.ClickPosition = new System.Drawing.Point()
            {
                X = 170,
                Y = 1546
            };

            SetGameState(GameState.ReOrderLevel);
        }

        private void TapScreen()
        {
            if (AppState.CurrentGameState != GameState.Transitioning)
            {
                //log.Info($"Tapping screen at X: {AppState.ClickPosition.X}, Y:{AppState.ClickPosition.Y}");

                (new TouchAction(_driver)).Tap(AppState.ClickPosition.X, AppState.ClickPosition.Y).Perform();

                System.Threading.Thread.Sleep(IMPLICIT_WAIT_TIME);

                SetGameState(GameState.Transitioning);
            }
        }


        private void SetGameState(GameState state)
        {
            AppState.PreviousGameState = AppState.CurrentGameState;
            AppState.CurrentGameState = state;

            lblGameStateValue.Invoke(new MethodInvoker(delegate { lblGameStateValue.Text = state.ToString(); }));
            lblIsFourWordsOnlyValue.Invoke(new MethodInvoker(delegate { lblIsFourWordsOnlyValue.Text = AppState.IsFourWordsOnly.ToString(); }));

        }


        private async Task SolveLevel(IEnumerable<Character> charsWithPosition)
        {
            if (charsWithPosition != null && charsWithPosition.Count() > 0)
            {
                string allChars = string.Concat(charsWithPosition.Select(x => x.Char).ToArray());
                IEnumerable<IEnumerable<string>> solution = await GetWordsSolution(allChars);

                if (solution != null)
                {
                    foreach (var solutionGroup in solution)
                    {
                        foreach (var word in solutionGroup)
                        {
                            if (word.Length == 3 && AppState.IsFourWordsOnly)
                            {
                                break;
                            }

                            foreach (var cwp in charsWithPosition)
                            {
                                cwp.IsSelected = false;
                            }

                            var wordChars = word.ToUpper().ToCharArray();

                            switch (_swipeMethod)
                            {
                                case SwipeMethod.Appium:
                                    WriteWordUsingAppium(charsWithPosition, wordChars);
                                    break;
                                case SwipeMethod.Native:
                                    WriteWordUsingMobileShell(charsWithPosition, wordChars);
                                    System.Threading.Thread.Sleep(_nativeSwipePausePerLetter * word.Length);
                                    break;
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(IMPLICIT_WAIT_TIME);
                SetGameState(GameState.Transitioning);
            }
        }

        private void WriteWordUsingMobileShell(IEnumerable<Character> charsWithPosition, char[] wordChars)
        {
            var charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(wordChars[0]));
            var firstCharPosition = charWithPosition.Position;
            charWithPosition.IsSelected = true;
            string wordScript = START_TOUCH_SCRIPT;
            wordScript += string.Format(POSITION_SCRIPT, firstCharPosition.X, firstCharPosition.Y);

            for (int charIndex = 1; charIndex < wordChars.Length; charIndex++)
            {

                charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(wordChars[charIndex]) && !x.IsSelected);

                charWithPosition.IsSelected = true;

                wordScript += string.Format(POSITION_SCRIPT, charWithPosition.Position.X, charWithPosition.Position.Y);
            }
            wordScript += END_TOUCH_SCRIPT;

            var args = new Dictionary<string, string>();
            args.Add("command", wordScript);
            _driver.ExecuteScript("mobile: shell", args);
        }


        private void WriteWordUsingAppium(IEnumerable<Character> charsWithPosition, char[] chars)
        {
            var charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[0]));
            var firstCharPosition = charWithPosition.Position;
            charWithPosition.IsSelected = true;

            var touchAction = (new TouchAction(_driver));
            touchAction.Press(firstCharPosition.X, firstCharPosition.Y);

            for (int charIndex = 1; charIndex < chars.Length; charIndex++)
            {
                charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[charIndex]) && !x.IsSelected);

                charWithPosition.IsSelected = true;

                touchAction.MoveTo(charWithPosition.Position.X, charWithPosition.Position.Y);
            }
            touchAction.Release().Perform();
        }

        private void _stateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _stateTimer.Enabled = false;

            if (AppState.IsFreshScreenshot)
            {
                //var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                //AppStore.BinarizedScreenshot.Save($"App_Data\\current_screen_binarized_state_thread{threadId}.png");

                switch (AppState.CurrentGameState)
                {
                    case GameState.Initializing:

                        //SetGameState(AppState.OriginalScreenshot);

                        lock (_originalScreenShotLock)
                        {
                            var wordPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, AppState.OriginalScreenshot, false, 400, 1410, 70, 600);

                            if (wordPosition == null)
                            {
                                wordPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, AppState.OriginalScreenshot, true, 400, 1410, 70, 600);
                            }

                            if (wordPosition != null)
                            {
                                //Click on that point to start the level
                                AppState.ClickPosition = new System.Drawing.Point()
                                {
                                    X = wordPosition.Value.Value.X1,
                                    Y = wordPosition.Value.Value.Y1
                                };

                                SetGameState(GameState.Menu);
                            }
                        }
                        break;
                    case GameState.Transitioning:
                    case GameState.Puzzle:
                        SetGameState(AppState.OriginalScreenshot);
                        break;
                }
                AppState.IsFreshScreenshot = AppState.CurrentGameState == AppState.PreviousGameState;
            }

            _stateTimer.Enabled = true;
        }

        private void _screenshotTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Pause timer
            _screenshotTimer.Enabled = false;

            if (AppState.CurrentGameState != GameState.Puzzle)
            {
                // log.Info("Getting phone screenshot");

                var currentScreenshot = _driver.GetScreenshot();
                using (var screenshotMemStream = new MemoryStream(currentScreenshot.AsByteArray))
                {
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    lock (_originalScreenShotLock)
                    {
                        AppState.OriginalScreenshot = new Bitmap(screenshotMemStream);
                        //AppState.OriginalScreenshot.Save($"App_Data\\current_screen_original_screenshot.png");

                        AppState.IsFreshScreenshot = true;

                        // log.Info("phone screenshot - saved");
                    }
                }

            }
            _screenshotTimer.Enabled = true;
        }


        private void SetGameState(Bitmap image)
        {
            lock (_originalScreenShotLock)
            {
                // log.Info($"Start - Set Game State - Current state:{AppState.CurrentGameState}");

                try
                {
                    //Check for continue words using binarized image
                    var foundContinueWord = CheckForContinueWords(image, false);
                    if (foundContinueWord)
                    {
                        return;
                    }


                    var foundPiggyBank = CheckForPiggyBank(image);
                    if (foundPiggyBank)
                    {
                        return;
                    }

                    if (AppState.CurrentGameState != GameState.Puzzle)
                    {

                        var levelControls = _ocr.GetCharacterControls(image);
                        if (levelControls != null && levelControls.Characters.Count() > 0)
                        {
                            if (!levelControls.ChangeOrder)
                            {
                                AppState.CurrentLevelControls = levelControls.Characters;
                                SetGameState(GameState.Puzzle);
                                return;
                            }
                            else
                            {
                                log.Info("Set re-order state. OCR problem.");

                                SetReorderState();
                            }
                        }
                    }

                    //Check for continue words for binarized image

                    foundContinueWord = CheckForContinueWords(image, true);
                    if (foundContinueWord)
                    {
                        return;
                    }
                }
                finally
                {
                    //  log.Info($"End - Game State: {AppState.CurrentGameState}");
                }
            }
        }

        private bool CheckForPiggyBank(Bitmap image)
        {
            var piggyPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string> { "PIGGY" }, image, true, 370, 600, 80, 380);
            if (piggyPosition != null)
            {
                var crossLocationOffsetY = 50;
                var crossLocationOffsetX = 750;

                AppState.ClickPosition = new System.Drawing.Point()
                {
                    X = piggyPosition.Value.Value.X1 + crossLocationOffsetX,
                    Y = piggyPosition.Value.Value.Y1 - crossLocationOffsetY
                };
                SetGameState(GameState.PiggyBank);
                return true;
            }

            return false;
        }

        private bool CheckForContinueWords(Bitmap image, bool binarizeImage)
        {
            var continueWords = new List<string> { "LEVEL", "COLLECT" };
            var matchingWord = _ocr.GetFirstMatchingWordCoordinates(continueWords, image, binarizeImage, OCR_MATHCING_CONTINUE_WORD_LEFT, OCR_MATHCING_CONTINUE_WORD_TOP, OCR_MATHCING_CONTINUE_WORD_HEIGHT, OCR_MATHCING_CONTINUE_WORD_WIDTH);
            if (matchingWord != null)
            {
                //Click on that point to start the level
                AppState.ClickPosition = new System.Drawing.Point()
                {
                    X = matchingWord.Value.Value.X1 + matchingWord.Value.Value.Width / 2,
                    Y = matchingWord.Value.Value.Y1 + matchingWord.Value.Value.Height / 2
                };

                SetGameState(GameState.LevelSolved);

                return true;
            }

            return false;
        }

        private async Task<IEnumerable<IEnumerable<string>>> GetWordsSolution(string letters)
        {
            using (var httpClient = new HttpClient())
            {
                var url = string.Format(WORDS_SERVICE_URL, letters);

                try
                {
                    var reponse = await httpClient.GetAsync(url);

                    var jsonResult = await reponse.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<IEnumerable<IEnumerable<string>>>(jsonResult);
                }
                catch
                {
                    return null;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtDeviceId.Enabled = false;

            SetGameState(GameState.Initializing);

            log.Info("Initializing WordScapes...");

            var driverOptions = new AppiumOptions();

            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, txtDeviceId.Text);
            // driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "emulator-5554");

            driverOptions.AddAdditionalCapability("appPackage", "com.peoplefun.wordcross");
            driverOptions.AddAdditionalCapability("appActivity", "com.peoplefun.wordcross.MonkeyGame");
            driverOptions.AddAdditionalCapability("noReset", "true");
            driverOptions.AddAdditionalCapability("disableWindowAnimation", "true");


            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions);

            SetTimers();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
            base.OnFormClosing(e);
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
            base.Dispose(disposing);
        }


        public class CharComparer : IEqualityComparer<Character>
        {
            public bool Equals(Character c1, Character c2)
            {
                if (c1.Char == c2.Char)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public int GetHashCode(Character obj)
            {
                return int.Parse(obj.Char.ToString());
            }
        }

        public enum SwipeMethod
        {
            Appium,
            Native
        }

        private void cbSwipeMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            _swipeMethod = (SwipeMethod)Enum.Parse(typeof(SwipeMethod), cbSwipeMethod.SelectedItem.ToString());

            txtNativeSwipePause.Enabled = _swipeMethod == SwipeMethod.Native;
        }

        private void txtNativeSwipePause_TextChanged(object sender, EventArgs e)
        {
            _nativeSwipePausePerLetter = int.Parse(txtNativeSwipePause.Text);
        }

        private void txtNativeSwipePause_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void WordscapesSolver_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }
    }
}
