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
            var levelControls = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(4, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('P')));
        }

        //[TestMethod]
        //public void UT_Controls_Detection_ARE()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.ARE.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(3, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        //}

        [TestMethod]
        public void UT_Controls_Detection_FAIR()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.FAIR.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var levelControls = _sut.GetCharacterControls(image);

            //Verify
            Assert.AreEqual(4, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
        }

        //[TestMethod]
        //public void UT_Controls_Detection_SAVED()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.SAVED.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(5, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('V')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        //}

        //[TestMethod]
        //public void UT_Controls_Detection_LEGALE()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LEGALE.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(6, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('L')) == 2);
        //    Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('E')) == 2);
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
        //}

        //[TestMethod]
        //public void UT_Controls_Detection_LUDGE()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LUDGE.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(5, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        //}

        //[TestMethod]
        //public void UT_Controls_Detection_WARMUP()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.LUDGE.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(5, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        //}

        [TestMethod]
        public void UT_Controls_Detection_HOUNDED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.HOUNDED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var levelControls = _sut.GetCharacterControls(binarizedImage);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('H')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('D')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }

        [TestMethod]
        public void UT_Controls_Detection_JUICIER()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.JUICIER.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

            //Execute
            var binarizedImage = _sut.Binarize(image);
            var levelControls = _sut.GetCharacterControls(binarizedImage);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('J')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
        }
    }
}
