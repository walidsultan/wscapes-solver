using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using WS.Wscapes;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace WordscapesSolver.OCR.Tests
{
    [TestClass]
    public class WordDetectionTests
    {

        [TestMethod]
        public void UT_Word_Detection_Collection_ND()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.collect_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, true, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }



        [TestMethod]
        public void UT_Word_Detection_Collection_ND2()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.collect_3.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, true, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
        }


        [TestMethod]
        public void UT_Word_Detection_LEVEL_2()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, true, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL_3()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_3.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            // var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, false, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL_4()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_4.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL_5()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_5.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }


        [TestMethod]
        public void UT_Word_Detection_LEVEL_6()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_6.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL_7()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_6.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_PIGGY()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.PiggyBank.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "PIGGY" }, image, true, dimensions.PiggyBankLeft, dimensions.PiggyBankTop, dimensions.PiggyBankHeight, dimensions.PiggyBankWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
        }


        [TestMethod]
        public void UT_Low_Res_Level_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.level.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }


        [TestMethod]
        [Ignore]
        public void UT_Low_Res_540_960_Level_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.level_540_960.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }
            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }


        [TestMethod]
        [Ignore]
        public void UT_Low_Res_540_960_Level_2_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.Level2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }


        [TestMethod]
        public void UT_Low_Res_540_960_Level_3_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.level_540_960_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_Level_100k_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.Level_100k.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, false, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, true, dimensions.OcrInitWordLeft, dimensions.OcrInitWordTop, dimensions.OcrInitWordHeight, dimensions.OcrInitWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("LEVEL", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_COLLECT_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.collect_540_960.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "COLLECT" }, image, false, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);
            if (matchingWord == null)
            {
                matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "COLLECT" }, image, true, dimensions.OcrContinueWordLeft, dimensions.OcrContinueWordTop, dimensions.OcrContinueWordHeight, dimensions.OcrContinueWordWidth);
            }

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_Word_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.TEAM.CHAT.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "CHAT" }, image, false, dimensions.ChatButtonLeft, dimensions.ChatButtonTop, dimensions.ChatButtonHeight, dimensions.ChatButtonWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("CHAT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_Collect_Stars_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.Collect.Stars.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "COLLECT" }, image, false, dimensions.OcrCollectStarsLeft, dimensions.OcrCollectStarsTop, dimensions.OcrCollectStarsHeight, dimensions.OcrCollectStarsWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_Collect_Stars_2_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.Collect.Stars.2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "COLLECT" }, image, false, dimensions.OcrCollectStarsLeft, dimensions.OcrCollectStarsTop, dimensions.OcrCollectStarsHeight, dimensions.OcrCollectStarsWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_Skip_Animation_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.Skip.Animation.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "SKIP" }, image, false, dimensions.OcrSkipAnimationLeft, dimensions.OcrSkipAnimationTop, dimensions.OcrSkipAnimationHeight, dimensions.OcrSkipAnimationWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("SKIP", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_Skip_Animation_2_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.Skip.Animation.2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "SKIP" }, image, false, dimensions.OcrSkipAnimationLeft, dimensions.OcrSkipAnimationTop, dimensions.OcrSkipAnimationHeight, dimensions.OcrSkipAnimationWidth);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("SKIP", matchingWord.Value.Key);
        }


        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_HELP_1_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.TEAM.CHAT.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "HELP" }, image, true, dimensions.HelpColumnLeft, dimensions.HelpColumnTop, dimensions.HelpColumnHeight, dimensions.HelpColumnWidth, Tesseract.PageSegMode.SparseText);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("HELP", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_HELP_2_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.TEAM.HELP.1.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "HELP" }, image, true, dimensions.HelpColumnLeft, dimensions.HelpColumnTop, dimensions.HelpColumnHeight, dimensions.HelpColumnWidth, Tesseract.PageSegMode.SparseText);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("HELP", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Low_Res_540_960_CHAT_HELP_3_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.TEAM.HELP.2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "HELP" }, image, true, dimensions.HelpColumnLeft, dimensions.HelpColumnTop, dimensions.HelpColumnHeight, dimensions.HelpColumnWidth, Tesseract.PageSegMode.SparseText);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("HELP", matchingWord.Value.Key);
        }

        [TestMethod]
        [Ignore]
        public void UT_Low_Res_540_960_CHAT_HELP_4_Detection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.TEAM.HELP.3.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "HELP" }, image, true, dimensions.HelpColumnLeft, dimensions.HelpColumnTop, dimensions.HelpColumnHeight, dimensions.HelpColumnWidth, Tesseract.PageSegMode.SparseText);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("HELP", matchingWord.Value.Key);
        }
    }
}
