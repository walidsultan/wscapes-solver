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
        public void UT_Controls_Detection_TEXTRUE()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.TEXTRUE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var binarizedImage = _sut.Binarize(image);
            binarizedImage.Save(@"APP_DATA\test.case.TEXTRUE_Binarized.png");
            var chars = _sut.GetCharacterControls(binarizedImage);

            //Verify
            Assert.AreEqual(7, chars.Count());
            Assert.IsNotNull(chars.Count(x => x.Char.Equals('T')) == 2);
            Assert.IsNotNull(chars.Count(x => x.Char.Equals('E')) == 2);
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('X')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('U')));
        }

    }
}
