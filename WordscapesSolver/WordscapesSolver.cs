using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
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


        private const int SCREENSHOT_TIMER_CONSTANT = 1000;
        private const int STATE_TIMER_CONSTANT = 1000;
        private const int ACTION_TIMER_CONSTANT = 100;
        public WordscapesSolver()
        {
            InitializeComponent();

            _ocr = new OCR();
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

            switch (AppStore.CurrentGameState)
            {
                case GameState.Menu:
                case GameState.LevelSolved:
                case GameState.PiggyBank:
                    TapScreen();
                    break;
                case GameState.Puzzle:
                    await SolveLevel(AppStore.LevelControls);
                    break;
            }
            _actionTimer.Enabled = true;

        }

        private void TapScreen()
        {
            if (AppStore.CurrentGameState != GameState.Transitioning)
            {
                (new TouchAction(_driver)).Tap(AppStore.ClickPosition.X, AppStore.ClickPosition.Y).Perform();

                SetGameState(GameState.Transitioning);
            }
        }


        private void SetGameState(GameState state)
        {
            AppStore.PreviousGameState = AppStore.CurrentGameState;
            AppStore.CurrentGameState = state;

            lblGameStateValue.Invoke(new MethodInvoker(delegate { lblGameStateValue.Text = state.ToString(); }));
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
                        if (AppStore.CurrentGameState != GameState.Puzzle)
                        {
                            break;
                        }

                        foreach (var cwp in charsWithPosition)
                        {
                            cwp.IsSelected = false;
                        }

                        var chars = word.ToUpper().ToCharArray();

                        var touchAction = (new TouchAction(_driver));

                        var charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[0]));
                        var firstCharPosition = charWithPosition.Position;
                        touchAction.Press(firstCharPosition.X, firstCharPosition.Y);
                        charWithPosition.IsSelected = true;
                        for (int charIndex = 1; charIndex < chars.Length; charIndex++)
                        {

                            charWithPosition = charsWithPosition.FirstOrDefault(x => x.Char.Equals(chars[charIndex]) && !x.IsSelected);

                            charWithPosition.IsSelected = true;

                            touchAction.MoveTo(charWithPosition.Position.X, charWithPosition.Position.Y);

                        }
                        touchAction.Release().Perform();
                    }
                }
            }
        }

        private void _stateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _stateTimer.Enabled = false;

            if (AppStore.BinarizedScreenshot != null)
            {
                var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                //AppStore.BinarizedScreenshot.Save($"App_Data\\current_screen_binarized_state_thread{threadId}.png");

                switch (AppStore.CurrentGameState)
                {
                    case GameState.Initializing:

                        SetGameState(AppStore.BinarizedScreenshot);


                        var wordPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, AppStore.BinarizedScreenshot);
                        if (wordPosition != null)
                        {
                            //Click on that point to start the level
                            AppStore.ClickPosition = new System.Drawing.Point()
                            {
                                X = wordPosition.Value.Value.X1,
                                Y = wordPosition.Value.Value.Y1
                            };

                            SetGameState(GameState.Menu);
                        }
                        break;
                    case GameState.Transitioning:
                    case GameState.Puzzle:
                        SetGameState(AppStore.BinarizedScreenshot);
                        break;
                }
            }

            _stateTimer.Enabled = true;
        }

        private void _screenshotTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Pause timer
            _screenshotTimer.Enabled = false;

            var currentScreenshot = _driver.GetScreenshot();
            using (var screenshotMemStream = new MemoryStream(currentScreenshot.AsByteArray))
            {
                //var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                AppStore.OriginalScreenshot = new Bitmap(screenshotMemStream);
                //  AppStore.OriginalScreenshot.Save($"App_Data\\current_screen_original_screenshot_thread{threadId}.png");
                AppStore.BinarizedScreenshot = _ocr.Binarize(AppStore.OriginalScreenshot);
                //    AppStore.BinarizedScreenshot.Save($"App_Data\\current_screen_binarized_screenshot_thread{threadId}.png");
            }

            _screenshotTimer.Enabled = true;
        }


        private void SetGameState(Bitmap binarizedImage)
        {

            if (AppStore.CurrentGameState == GameState.Puzzle)
            {
                var continueWords = new List<string> { "LEVEL", "COLLECT", "PIGGY" };
                var matchingWord = _ocr.GetFirstMatchingWordCoordinates(continueWords, binarizedImage, 1400);
                if (matchingWord != null)
                {
                    //Click on that point to start the level
                    AppStore.ClickPosition = new System.Drawing.Point()
                    {
                        X = matchingWord.Value.Value.X1,
                        Y = matchingWord.Value.Value.Y1
                    };

                    SetGameState(GameState.LevelSolved);

                    return;
                }

                var foundPiggyBank = CheckForPiggyBank(binarizedImage);
                if (foundPiggyBank) {
                    return;
                }
            }


            if (AppStore.CurrentGameState == GameState.Transitioning)
            {
                var charsWithPosition = _ocr.GetCharacterControls(binarizedImage);
                if (charsWithPosition != null && charsWithPosition.Count() > 0)
                {
                    AppStore.LevelControls = charsWithPosition;
                    SetGameState(GameState.Puzzle);
                    return;
                }

                CheckForPiggyBank(binarizedImage);
            }
        }

        private bool CheckForPiggyBank(Bitmap binarizedImage)
        {
            var piggyPosition = _ocr.GetFirstMatchingWordCoordinates(new List<string> { "PIGGY" }, binarizedImage);
            if (piggyPosition != null)
            {
                var crossLocationOffsetY = 50;
                var crossLocationOffsetX = 750;

                AppStore.ClickPosition = new System.Drawing.Point()
                {
                    X = piggyPosition.Value.Value.X1 + crossLocationOffsetX,
                    Y = piggyPosition.Value.Value.Y1 - crossLocationOffsetY
                };
                SetGameState(GameState.PiggyBank);
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
            AppStore.CurrentGameState = GameState.Initializing;

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
    }
}
