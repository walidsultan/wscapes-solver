using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using WS.Wscapes;
using System.Linq;
using System.IO;

namespace WordscapesSolver.OCR.Tests
{
    [TestClass]
    public class BinarizeTests
    {

        [TestMethod]
        public void UT_Controls_Detection_4WordOnly()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\IsFourWordsOnly_Before_3.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var isFourWordsOnly = _sut.IsFourWordsOnly(image, dimensions);

            //Verify
            Assert.IsTrue(isFourWordsOnly);

            //Execute
            image = new Bitmap(@"TestCases\NotFourWordsOnly_Before.png");
            isFourWordsOnly = _sut.IsFourWordsOnly(image, dimensions);
            //Verify
            Assert.IsFalse(isFourWordsOnly);
        }


        [TestMethod]
        public void UT_Low_Res_540_960_Controls_Detection_4WordOnly()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\LowRes\test.case.540_960.NOTIFY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var isFourWordsOnly = _sut.IsFourWordsOnly(image, dimensions);

            //Verify
            Assert.IsTrue(isFourWordsOnly);

            //Execute
            image = new Bitmap(@"TestCases\LowRes\test.case.540_960.DITEEC.png");
            isFourWordsOnly = _sut.IsFourWordsOnly(image, dimensions);
            //Verify
            Assert.IsFalse(isFourWordsOnly);
        }

    }
}
