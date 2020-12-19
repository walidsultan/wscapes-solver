using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using WS.Wscapes;
using System.Linq;
using System.IO;

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
            var rect = _sut.GetWordCoordinates("LEVEL", image,600);

            //Verify
            Assert.IsNull(rect);

            //Execute
            rect = _sut.GetWordCoordinates("COLLECT", image);

            //Verify
            Assert.IsNotNull(rect);
        }

        [TestMethod]
        public void UT_Word_Detection_LEVEL()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.level.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var rect = _sut.GetWordCoordinates("LEVEL", image,600);

            //Verify
            Assert.IsNotNull(rect);
        }

        [TestMethod]
        public void UT_Word_Detection_PIGGY()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.PiggyBank.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var rect = _sut.GetWordCoordinates("PIGGY", image, 600);

            //Verify
            Assert.IsNotNull(rect);
        }
    }
}
