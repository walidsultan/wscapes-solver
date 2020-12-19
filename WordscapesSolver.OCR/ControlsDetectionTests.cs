using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using WS.Wscapes;
using System.Linq;
using System.IO;

namespace WordscapesSolver.OCR.Tests
{
    [TestClass]
    public class ControlsDetectionTests
    {
        [TestMethod]
        public void UT_Controls_Detection_DRIP()
        {
            //Setup
            Bitmap image = new Bitmap(@"TestCases\test.case.DRIP.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(4, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("D", System.StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("R", System.StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("I", System.StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("P", System.StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void UT_Controls_Detection_ARE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory()+ @"\TestCases\test.case.ARE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(3, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("A", System.StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("R", System.StringComparison.OrdinalIgnoreCase)));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals("E", System.StringComparison.OrdinalIgnoreCase)));
        }
    }
}
