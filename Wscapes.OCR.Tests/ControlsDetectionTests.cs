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
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(4, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('P')));
        }

        //[TestMethod]
        //public void UT_Controls_Detection_FAIR()
        //{
        //    //Setup
        //    Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.FAIR.png");
        //    WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();

        //    //Execute
        //    var levelControls = _sut.GetCharacterControls(image);

        //    //Verify
        //    Assert.AreEqual(4, levelControls.Characters.Count());
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
        //    Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
        //}

        [TestMethod]
        public void UT_Controls_Detection_HOUNDED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.HOUNDED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

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
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('J')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
        }



        [TestMethod]
        public void UT_Controls_Detection_WARMER()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.WARMER.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('W')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('R')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }




        [TestMethod]
        public void UT_Controls_Detection_SILENCE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.SILENCE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('E')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
        }

        [TestMethod]
        public void UT_Controls_Detection_MORGUE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.MORGUE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }

        [TestMethod]
        public void UT_Controls_Detection_FORRVE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.FORRVE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('R')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('V')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }


        [TestMethod]
        public void UT_Controls_Detection_SENTRY()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.SENTRY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }

        [TestMethod]
        public void UT_Controls_Detection_SAVED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\test.case.SAVED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(5, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('V')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        }


        [TestMethod]
        public void UT_Low_Res_Controls_Detection_DITEEC()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.DITEEC.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('E')) == 2);
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_NOTIFY()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.NOTIFY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_DISTILL()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.DISTILL.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('L')) == 2);
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_DUNKED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.DUNKED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('D')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('K')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }


        [TestMethod]
        public void UT_Low_Res_Controls_Detection_SPECIFY()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.SPECIFY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('S')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('P')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }


        [TestMethod]
        public void UT_Low_Res_Controls_Detection_CHALICE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.CHALICE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('C')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('H')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }


        [TestMethod]
        public void UT_Low_Res_Controls_Detection_TIMIDLY()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.TIMIDLY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }


        [TestMethod]
        public void UT_Low_Res_Controls_Detection_TIMIDLY2()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.TIMIDLY_2.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }
        [TestMethod]
        public void UT_Low_Res_Controls_Detection_REWARD()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.REWARD.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('R')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('W')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        }

        [Ignore]
        [TestMethod]
        public void UT_Low_Res_Controls_Detection_CONVICT()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.CONVICT.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('C')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('V')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_OCCUPY()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.OCCUPY.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('C')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('P')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Y')));
        }

        [Ignore]
        [TestMethod]
        public void UT_Low_Res_Controls_Detection_INFLOW()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.INFLOW.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('F')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('O')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('W')));
        }

        [Ignore]
        [TestMethod]
        public void UT_Low_Res_Controls_Detection_DIGITAL()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.DIGITAL.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('I')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('T')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('A')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_MUDBIE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.MUDBIE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(6, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('B')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_MUFFLED()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.MUFFLED.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('M')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('F')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('D')));
        }

        [TestMethod]
        public void UT_Low_Res_Controls_Detection_EXTINCT()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\LowRes\test.case.540_960.EXTINCT.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('X')));
            Assert.IsNotNull(levelControls.Characters.Count(x => x.Char.Equals('T')) == 2);
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('I')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('N')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
        }

        [TestMethod]
        [Ignore]
        public void UT_Hi_Res_Controls_Detection_CLARQUE()
        {
            //Setup
            Bitmap image = new Bitmap(Directory.GetCurrentDirectory() + @"\TestCases\HiRes\test.case.CLARQUE.png");
            WS.Wscapes.OCR _sut = new WS.Wscapes.OCR();
            var dimensions = new Dimensions(image.Width, image.Height);

            //Execute
            var levelControls = _sut.GetCharacterControls(image, dimensions);

            //Verify 
            Assert.AreEqual(7, levelControls.Characters.Count());
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('C')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('L')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('G')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('R')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('Q')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('U')));
            Assert.IsNotNull(levelControls.Characters.Single(x => x.Char.Equals('E')));
        }
    }
}
