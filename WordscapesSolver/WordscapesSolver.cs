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

        private const int SCREENSHOT_TIMER_CONSTANT = 1000;
        private const int STATE_TIMER_CONSTANT = 100;
        private const int ACTION_TIMER_CONSTANT = 100;

        private const string START_TOUCH_SCRIPT = "su -c 'sendevent /dev/input/event2 3 57 13;";
        private const string POSITION_SCRIPT = "sendevent /dev/input/event2 3 53 {0};sendevent /dev/input/event2 3 54 {1};sendevent /dev/input/event2 3 58 17;sendevent /dev/input/event2 0 0 0;";
        private const string END_TOUCH_SCRIPT = "sendevent /dev/input/event2 3 57 -1;sendevent /dev/input/event2 0 0 0;'";
        public WordscapesSolver()
        {
            InitializeComponent();

            _ocr = new OCR();
            _charComparer = new CharComparer();
        }

        private void WordscapesSolver_Load(object sender, EventArgs e)
        {
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
                        SetReorderState();
                    }
                    else
                    {
                        AppState.PreviousLevelControls = AppState.CurrentLevelControls.ToList();
                        await SolveLevel(AppState.CurrentLevelControls);
                    }
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
                (new TouchAction(_driver)).Tap(AppState.ClickPosition.X, AppState.ClickPosition.Y).Perform();

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

                        var chars = word.ToUpper().ToCharArray();


                        var charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[0]));
                        var firstCharPosition = charWithPosition.Position;
                        charWithPosition.IsSelected = true;
                        string wordScript = START_TOUCH_SCRIPT;
                        wordScript += string.Format(POSITION_SCRIPT, firstCharPosition.X, firstCharPosition.Y);

                        for (int charIndex = 1; charIndex < chars.Length; charIndex++)
                        {

                            charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[charIndex]) && !x.IsSelected);

                            charWithPosition.IsSelected = true;

                            wordScript += string.Format(POSITION_SCRIPT, charWithPosition.Position.X, charWithPosition.Position.Y);
                        }
                        wordScript += END_TOUCH_SCRIPT;

                        var args = new Dictionary<string, string>();
                        args.Add("command", wordScript);
                        _driver.ExecuteScript("mobile: shell", args);

                    }
                }

                System.Threading.Thread.Sleep(3000);
                SetGameState(GameState.Transitioning);
            }
        }

        private void _stateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _stateTimer.Enabled = false;

            if (AppState.BinarizedScreenshot != null && AppState.IsFreshScreenshot)
            {
                //var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                //AppStore.BinarizedScreenshot.Save($"App_Data\\current_screen_binarized_state_thread{threadId}.png");

                switch (AppState.CurrentGameState)
                {
                    case GameState.Initializing:

                        SetGameState(AppState.BinarizedScreenshot);


                        var wordPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, AppState.BinarizedScreenshot);
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
                        break;
                    case GameState.Transitioning:
                    case GameState.Puzzle:
                        SetGameState(AppState.BinarizedScreenshot);
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
                var currentScreenshot = _driver.GetScreenshot();
                using (var screenshotMemStream = new MemoryStream(currentScreenshot.AsByteArray))
                {
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    lock (_originalScreenShotLock)
                    {
                        AppState.OriginalScreenshot = new Bitmap(screenshotMemStream);
                        AppState.OriginalScreenshot.Save($"App_Data\\current_screen_original_screenshot_thread{threadId}.png");

                        AppState.BinarizedScreenshot = _ocr.Binarize(AppState.OriginalScreenshot);
                    }
                    //    AppStore.BinarizedScreenshot.Save($"App_Data\\current_screen_binarized_screenshot_thread{threadId}.png");

                    AppState.IsFreshScreenshot = true;
                }

            }
            _screenshotTimer.Enabled = true;
        }


        private void SetGameState(Bitmap binarizedImage)
        {
            //Check for continue words using binarized image
            var foundContinueWord = CheckForContinueWords(binarizedImage);
            if (foundContinueWord)
            {
                return;
            }


            var foundPiggyBank = CheckForPiggyBank(binarizedImage);
            if (foundPiggyBank)
            {
                return;
            }

            if (AppState.CurrentGameState != GameState.Puzzle)
            {

                var levelControls = _ocr.GetCharacterControls(binarizedImage);
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
                        SetReorderState();
                    }
                }
            }

            //Check for continue words using original screenshot
            lock (_originalScreenShotLock)
            {
                foundContinueWord = CheckForContinueWords(AppState.OriginalScreenshot);
                if (foundContinueWord)
                {
                    return;
                }
            }
        }

        private bool CheckForPiggyBank(Bitmap binarizedImage)
        {
            var piggyPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string> { "PIGGY" }, binarizedImage);
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

        private bool CheckForContinueWords(Bitmap image)
        {
            var continueWords = new List<string> { "LEVEL", "COLLECT" };
            var matchingWord = _ocr.GetFirstMatchingWordCoordinates(continueWords, image, 2020, 70);
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
                var url = $"http://localhost/wscapes/solve?letters={letters}";

                var reponse = await httpClient.GetAsync(url);

                var jsonResult = await reponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<IEnumerable<string>>>(jsonResult);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            AppState.CurrentGameState = GameState.Initializing;

            var driverOptions = new AppiumOptions();

            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, txtDeviceId.Text);
            // driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "emulator-5554");

            driverOptions.AddAdditionalCapability("appPackage", "com.peoplefun.wordcross");
            driverOptions.AddAdditionalCapability("appActivity", "com.peoplefun.wordcross.MonkeyGame");
            driverOptions.AddAdditionalCapability("noReset", "true");

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
    }
}
