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
        private static System.Timers.Timer _timer;
        private OCR _ocr;

        private GameState _gameState;
        public WordscapesSolver()
        {
            InitializeComponent();

            _ocr = new OCR();
        }

        private void WordscapesSolver_Load(object sender, EventArgs e)
        {


        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            _timer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private async void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Pause timer
            _timer.Enabled = false;

            var currentScreenshot = _driver.GetScreenshot();
            //currentScreenshot.SaveAsFile("App_Data\\current_screen.png");


            using (var screenshotMemStream = new MemoryStream(currentScreenshot.AsByteArray))
            {
                Bitmap screenshot = new Bitmap(screenshotMemStream);
                // screenshot.Save("App_Data\\current_screen.png");
                Bitmap screenshot_segmented = _ocr.Binarize(screenshot);
                screenshot_segmented.Save("App_Data\\current_screen_binarized.png");

                //var text = ocrPage.GetText();

                switch (_gameState)
                {
                    case GameState.Menu:
                        var wordPosition = _ocr.GetWordCoordinates("LEVEL", screenshot_segmented);
                        if (wordPosition != null)
                        {
                            //Click on that point to start the level
                            (new TouchAction(_driver)).Tap(wordPosition.Value.X1, wordPosition.Value.Y1).Perform();
                            _gameState = GameState.Puzzle;
                        }
                        break;
                    case GameState.LevelSolved:
                        IsLevelSolved(screenshot_segmented);
                        break;
                    case GameState.Puzzle:
                        var charsWithPosition = _ocr.GetCharacterControls(screenshot_segmented);
                        if (charsWithPosition != null && charsWithPosition.Count() > 0)
                        {
                            string allChars = string.Concat(charsWithPosition.Select(x => x.Char).ToArray());   // .Aggregate((x, y) => string.Concat(x, y));
                            IEnumerable<IEnumerable<string>> solution = await GetWordsSolution(allChars);

                            foreach (var solutionGroup in solution)
                            {

                                foreach (var word in solutionGroup)
                                {
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
                            _gameState = GameState.LevelSolved;
                        }
                        break;
                }

                //using (var stringWriter = new StringWriter())
                //{
                //    //stringWriter.WriteLine("Mean confidence: {0}", ocrPage.GetMeanConfidence());

                //    //stringWriter.WriteLine("Text (GetText): \r\n{0}", text);
                //    stringWriter.WriteLine("Text (iterator):");


                //    string stext = stringWriter.ToString();
                //}

            }

            //resume timer
            _timer.Enabled = true;

        }

        private void IsLevelSolved(Bitmap screenshot_segmented)
        {
            var levelPosition = _ocr.GetWordCoordinates("LEVEL", screenshot_segmented, 600);
            if (levelPosition != null)
            {
                //Click on that point to start the level
                (new TouchAction(_driver)).Tap(levelPosition.Value.X1, levelPosition.Value.Y1).Perform();
                _gameState = GameState.Puzzle;
            }

            var collectPosition = _ocr.GetWordCoordinates("COLLECT", screenshot_segmented);
            if (collectPosition != null)
            {
                //Click on that point to start the level
                (new TouchAction(_driver)).Tap(collectPosition.Value.X1, collectPosition.Value.Y1).Perform();
                _gameState = GameState.Puzzle;
            }

            var charsWithPosition = _ocr.GetCharacterControls(screenshot_segmented);
            if (charsWithPosition != null && charsWithPosition.Count() > 0)
            {
                _gameState = GameState.Puzzle;
            }

            var piggyPosition = _ocr.GetWordCoordinates("PIGGY", screenshot_segmented);
            if (piggyPosition != null)
            {
                var crossLocationOffsetY = 50;
                var crossLocationOffsetX = 750;

                //Close the piggy bank modal
                (new TouchAction(_driver)).Tap(collectPosition.Value.X1+crossLocationOffsetX, collectPosition.Value.Y1-crossLocationOffsetY).Perform();
            }
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
            _gameState = GameState.Menu;

            var driverOptions = new AppiumOptions();

            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, txtDeviceId.Text);
            // driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "emulator-5554");

            driverOptions.AddAdditionalCapability("appPackage", "com.peoplefun.wordcross");
            driverOptions.AddAdditionalCapability("appActivity", "com.peoplefun.wordcross.MonkeyGame");
            driverOptions.AddAdditionalCapability("noReset", "true");

            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions);

            SetTimer();
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
