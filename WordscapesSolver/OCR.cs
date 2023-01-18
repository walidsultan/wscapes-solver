using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tesseract;
using WS.Wscapes.DataTypes;

namespace WS.Wscapes
{
    public class OCR : IDisposable
    {
        private TesseractEngine _ocrEngine;
        public OCR()
        {
            _ocrEngine = new TesseractEngine(@"./App_Data/tessdata", "eng", EngineMode.Default);
        }


        public LevelControls GetCharacterControls(Bitmap screenshot_segmented, Dimensions dimensions)
        {

            //Pixel XL
            int controlOffsetLeft = dimensions.OcrControlsLeft;
            int controlOffsetTop = dimensions.OcrControlsTop;
            int controlOffsetWidth = dimensions.OcrControlsWidth;
            int controlOffsetHeight = dimensions.OcrControlsHeight;

            var controlsImage = CropImage(screenshot_segmented, new Rectangle(controlOffsetLeft, controlOffsetTop, controlOffsetWidth, controlOffsetHeight));
            //controlsImage.Save($"App_Data\\current_cropped_controls.png");

            var invertedControls = Binarize(controlsImage, false);
            invertedControls.Save($"App_Data\\current_cropped_controls_inverted.png");

            // process image with blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(invertedControls);
            IEnumerable<Blob> blobs = blobCounter.GetObjectsInformation().Where(x => x.Area > dimensions.OcrControlsMinArea && x.Area < dimensions.OcrControlsMaxArea && (x.Rectangle.Height >= dimensions.OcrControlsMinHeight & x.Rectangle.Height < dimensions.OcrControlsMaxHeight));

            if (blobs.Count() == 0) return null;

            //Cut the Characters
            var cropPadding = dimensions.OcrControlsPadding;
            List<Bitmap> charImages = new List<Bitmap>();
            foreach (var blob in blobs)
            {
                charImages.Add(CropImage(controlsImage, AddPadding(blob.Rectangle, cropPadding)));
            }

            //stack the images
            int outputImageWidth = 0;
            int outputImageHeight = 0;
            foreach (var img in charImages)
            {
                outputImageWidth += img.Width;
                outputImageHeight = img.Height > outputImageHeight ? img.Height : outputImageHeight;
            }

            Bitmap stackedImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(stackedImage))
            {
                int left = 0;
                foreach (var img in charImages)
                {
                    graphics.DrawImage(img, new Rectangle(new Point(left, 0), img.Size),
                            new Rectangle(new Point(), img.Size), GraphicsUnit.Pixel);
                    left += img.Width;
                }
            }
            stackedImage.Save($"App_Data\\stacked_cropped_controls.png");

            //Binarize stacked image
            stackedImage = Binarize(stackedImage);
            stackedImage.Save($"App_Data\\stacked_cropped_controls_binarized.png");

            LevelControls levelControls = new LevelControls();
            using (var ocrPage = _ocrEngine.Process(stackedImage, PageSegMode.SingleWord))
            {
                char[] chars = ocrPage.GetText().Trim().Replace(" ", "").ToUpper().ToCharArray();

                if (chars.Count() < 3) return null;

                if (chars.Count() != blobs.Count()) levelControls.ChangeOrder = true;

                levelControls.Characters = new List<Character>();
                int blobIndex = 0;
                foreach (var blob in blobs)
                {
                    if (blobIndex >= chars.Count()) break;

                    levelControls.Characters.Add(new Character()
                    {
                        Char = chars[blobIndex],
                        Position = new Rectangle(blob.Rectangle.X + controlOffsetLeft + blob.Rectangle.Width / 4, blob.Rectangle.Y + controlOffsetTop + blob.Rectangle.Height / 8, blob.Rectangle.Width, blob.Rectangle.Height)
                    });
                    blobIndex++;
                }
            }

            AppState.IsFourWordsOnly = IsFourWordsOnly(screenshot_segmented, dimensions);
            return levelControls;

        }

        private Rectangle AddPadding(Rectangle rectangle, int padding)
        {
            return new Rectangle(rectangle.X - padding, rectangle.Y - padding, rectangle.Width + 2 * padding, rectangle.Height + 2 * padding);
        }

        private Bitmap Binarize(Bitmap image, bool invert = true)
        {
            Bitmap sharpenImage = new Bitmap(image.Width, image.Height);

            int w = image.Width;
            int h = image.Height;

            int highThreshold = 255;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {

                    if ((image.GetPixel(x, y).R >= highThreshold && image.GetPixel(x, y).G >= highThreshold && image.GetPixel(x, y).B >= highThreshold) ||
                        (image.GetPixel(x, y).R == 0 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).B == 0) ||
                        (image.GetPixel(x, y).R == 96 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).B == 46))
                    {
                        sharpenImage.SetPixel(x, y, invert ? Color.Black : Color.White);
                    }
                    else
                    {
                        sharpenImage.SetPixel(x, y, invert ? Color.White : Color.Black);
                    }
                }
            }

            return sharpenImage;
        }


        public bool IsFourWordsOnly(Bitmap image, Dimensions dimensions)
        {
            var squareWidth = dimensions.IsFourWordsWidth;
            var squareHeight = dimensions.IsFourWordsHeight;
            image.Save($"App_Data\\IsFourWordsOnly_Before.png");

            var temp = Binarize(image);
            temp.Save($"App_Data\\IsFourWordsOnly_Before_2.png");

            var fourWordsOnly = CropImage(image, new Rectangle(dimensions.IsFourWordsLeft, image.Height - dimensions.IsFourWordsTop, squareWidth, squareHeight));
            fourWordsOnly = Binarize(fourWordsOnly);

            fourWordsOnly.Save($"App_Data\\IsFourWordsOnly_After.png");

            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(fourWordsOnly);

            return blobCounter.GetObjectsInformation().Count() > 1;

        }


        public KeyValuePair<string, Rect>? GetFirstMatchingWordCoordinates(List<string> words, Bitmap image, bool binarizeImage, int? xOffset = null, int? YOffset = null, int? height = null, int? width = null, PageSegMode segmentationMode = PageSegMode.SingleLine)
        {
            if (YOffset.HasValue || xOffset.HasValue)
            {
                image = CropImage(image, new Rectangle(xOffset ?? 0, YOffset.Value, width ?? image.Width, height ?? image.Height - YOffset.Value));
                image.Save("App_Data\\GetFirstMatchingWordCoordinates.png");
            }

            //binarize image
            var ocrImage = image;
            if (binarizeImage)
            {
                ocrImage = Binarize(image);
                ocrImage.Save("App_Data\\GetFirstMatchingWordCoordinates_binarized.png");
            }

            using (var ocrPage = _ocrEngine.Process(ocrImage, segmentationMode))
            {
                using (var iter = ocrPage.GetIterator())
                {
                    iter.Begin();
                    do
                    {
                        string ocrWord = iter.GetText(PageIteratorLevel.Word);

                        if (ocrWord != null)
                        {
                            string matchingWord = words.FirstOrDefault(x => ocrWord.Contains(x));

                            if (matchingWord != null)
                            {
                                iter.TryGetBoundingBox(PageIteratorLevel.Word, out Rect wordPosition);
                                if (YOffset.HasValue)
                                {
                                    wordPosition = new Rect(wordPosition.X1 + xOffset.Value, wordPosition.Y1 + YOffset.Value, wordPosition.Width, wordPosition.Height);
                                }
                                return new KeyValuePair<string, Rect>(matchingWord, wordPosition);
                            }
                        }
                    } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));
                }
            }
            return null;
        }

        private List<BitmapWithYOffset> CutRows(Bitmap image)
        {

            int w = image.Width;
            int h = image.Height;

            List<BitmapWithYOffset> dividedImages = new List<BitmapWithYOffset>();

            int divideThreshold = 5;
            int divideThresholdCount = 0;
            //int threshold = 255;
            int threshold = 0;


            int firstBlackPixelTop = 0;
            bool isPreviousAllWhitePixels = true;
            int padding = 10;
            for (int y = 0; y < h; y++)
            {
                bool isAllWhitePixels = true;

                for (int x = 0; x < w; x++)
                {
                    if (image.GetPixel(x, y).R != threshold || image.GetPixel(x, y).G != threshold || image.GetPixel(x, y).B != threshold)
                    {
                        if (isPreviousAllWhitePixels) firstBlackPixelTop = y;
                        isAllWhitePixels = false;
                        break;
                    }
                }
                isPreviousAllWhitePixels = isAllWhitePixels;

                if (isAllWhitePixels && firstBlackPixelTop > 0)
                {
                    divideThresholdCount++;
                    if (divideThresholdCount == divideThreshold)
                    {
                        Bitmap imageSubset = new Bitmap(image);
                        imageSubset = CropImage(imageSubset, new Rectangle(0, firstBlackPixelTop - padding, w, y - firstBlackPixelTop - divideThreshold + 2 * padding));
                        dividedImages.Add(new BitmapWithYOffset()
                        {
                            Image = imageSubset,
                            YOffset = firstBlackPixelTop - padding
                        });
                        divideThresholdCount = 0;
                        firstBlackPixelTop = 0;
                    }
                }
            }

            return dividedImages;
        }

        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap croppedImage = new Bitmap(img);

            if (cropArea.X + cropArea.Width > img.Width)
            {
                cropArea.Width = img.Width - cropArea.X;
            }

            if (cropArea.X < 0)
            {
                cropArea.Width += cropArea.X;
                cropArea.X = 0;
            }

            Crop filter = new Crop(cropArea);
            // apply the filter
            return filter.Apply(croppedImage);

            // return img.Clone(cropArea, img.PixelFormat);
        }

        private static bool IsAlphabet(string character)
        {
            return Regex.IsMatch(character.ToString(), "[a-z]", RegexOptions.IgnoreCase);
        }

        private static void Invert(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            int threshold = 255;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (image.GetPixel(x, y).R == threshold)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_ocrEngine != null)
            {
                _ocrEngine.Dispose();
            }
        }
    }

}
