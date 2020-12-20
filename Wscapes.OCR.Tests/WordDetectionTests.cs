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
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL", "COLLECT" }, image,600);

            //Verify
            Assert.IsNotNull(matchingWord);
            Assert.AreEqual("COLLECT", matchingWord.Value.Key);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var matchingWord = _sut.GetFirstMatchingWordCoordinates(new List<string>() { "LEVEL" }, image,600);

            //Verify
            Assert.IsNotNull(matchingWord);
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
