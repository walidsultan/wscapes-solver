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
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('P')));
        }

        [TestMethod]
        public void UT_Controls_Detection_ARE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.ARE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(3, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('E')));
        }

        [TestMethod]
        public void UT_Controls_Detection_FAIR()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.FAIR.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(4, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('R')));
        }

        [TestMethod]
        public void UT_Controls_Detection_SAVED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.SAVED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(5, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('V')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('D')));
        }

        [TestMethod]
        public void UT_Controls_Detection_LEGALE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LEGALE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(6, chars.Count());
            Assert.IsNotNull(chars.Count(x => x.Char.Equals('L')) == 2);
            Assert.IsNotNull(chars.Count(x => x.Char.Equals('E')) == 2);
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('A')));
        }

        [TestMethod]
        public void UT_Controls_Detection_LUDGE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LUDGE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(5, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('E')));
        }

        [TestMethod]
        public void UT_Controls_Detection_WARMUP()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LUDGE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var chars = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(5, chars.Count());
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(chars.Single(x => x.Char.Equals('E')));
        }
    }
}
