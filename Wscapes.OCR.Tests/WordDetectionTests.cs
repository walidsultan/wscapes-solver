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
        public void UT_Word_Detection_Collection()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.collect.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, 1600);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_Collection_ND()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.collect_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, binarizedImage, 2020, 70);

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

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, binarizedImage, 2020, 70);

            //Verify
            Assert.IsNotNull(matchingWord);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image, 600);

            //Verify
            Assert.IsNotNull(matchingWord);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL_2()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, binarizedImage, 2020, 70);

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

            //Execute
            // var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image, 2020, 70);

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

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL"}, binarizedImage, 2020, 70);

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

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "PIGGY" }, image, 600);

            //Verify
            Assert.IsNotNull(matchingWord);
        }
    }
}
