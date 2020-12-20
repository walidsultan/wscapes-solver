﻿using AForge.Imaging;
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

        //public IEnumerable<Character> GetCharacterControls(Bitmap screenshot_segmented)
        //{
        //    int controlOffsetLeft = 227;
        //    int controlOffsetTop = 1500;
        //    //int controlOffsetWidth = 524;
        //    int controlOffsetWidth = 644;
        //    //int controlOffsetHeight = 503;
        //    int controlOffsetHeight = 753;

        //    screenshot_segmented = CropImage(screenshot_segmented, new Rectangle(controlOffsetLeft, controlOffsetTop, controlOffsetWidth, controlOffsetHeight));
        //    screenshot_segmented.Save($"App_Data\\current_cropped_controls.png");

        //    var dividedImages = CutRows(screenshot_segmented);
        //    int count = 0;
        //    List<Character> ocrChars = new List<Character>();
        //    foreach (var img in dividedImages)
        //    {
        //        img.Image.Save($"App_Data\\current_screenshot_cut{count}.png");
        //        count++;

        //        using (var ocrPage = _ocrEngine.Process(img.Image, PageSegMode.SingleLine))
        //        {
        //            //var text = ocrPage.GetText();
        //            using (var iter = ocrPage.GetIterator())
        //            {
        //                iter.Begin();
        //                //stringWriter.WriteLine(iter.GetText(PageIteratorLevel.Word));
        //                do
        //                {
        //                    string ocrWord = iter.GetText(PageIteratorLevel.Symbol);

        //                    if (string.IsNullOrEmpty(ocrWord)) continue;

        //                    if (IsAlphabet(ocrWord))
        //                    {
        //                        iter.TryGetBoundingBox(PageIteratorLevel.Word, out Rect characterPosition);

        //                        ocrChars.Add(new Character()
        //                        {
        //                            Char = ocrWord.Trim(),
        //                            Position = new Rect(characterPosition.X1 + controlOffsetLeft + characterPosition.Width / 2, characterPosition.Y1 + controlOffsetTop + img.YOffset + characterPosition.Height / 2, characterPosition.Width, characterPosition.Height)
        //                        });
        //                    }
        //                } while (iter.Next(PageIteratorLevel.Symbol));

        //            }
        //        }
        //    }

        //    return ocrChars;
        //}


        public IEnumerable<Character> GetCharacterControls(Bitmap screenshot_segmented)
        {
            ////Emulator
            //int controlOffsetLeft = 227;
            //int controlOffsetTop = 1400;
            //int controlOffsetWidth = 644;
            //int controlOffsetHeight = 840;

            //Pixel XL
            int controlOffsetLeft = 227;
            int controlOffsetTop = 1400;
            int controlOffsetWidth = 954;
            int controlOffsetHeight = 1040;

            screenshot_segmented = CropImage(screenshot_segmented, new Rectangle(controlOffsetLeft, controlOffsetTop, controlOffsetWidth, controlOffsetHeight));
            screenshot_segmented.Save($"App_Data\\current_cropped_controls.png");

            var inverted_controls = (Bitmap)screenshot_segmented.Clone();
            Invert(inverted_controls);
            inverted_controls.Save($"App_Data\\current_cropped_controls_inverted.png");


            // process image with blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(inverted_controls);
            IEnumerable<Blob> blobs = blobCounter.GetObjectsInformation().Where(x => x.Area > 2500 && x.Area<13500);
            if (blobs.Count() == 0) return null;

            //Cut the Characters
            var cropPadding = 10;
            List<Bitmap> charImages = new List<Bitmap>();
            foreach (var blob in blobs)
            {
                charImages.Add(CropImage(screenshot_segmented, AddPadding(blob.Rectangle, cropPadding)));
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


            using (var ocrPage = _ocrEngine.Process(stackedImage, PageSegMode.SingleWord))
            {
                char[] chars = ocrPage.GetText().Trim().Replace(" ", "").ToUpper().ToCharArray();

                if (chars.Count() != blobs.Count()) return null;

                List<Character> charsWithPosition = new List<Character>();
                int blobIndex = 0;
                foreach (var blob in blobs)
                {
                    charsWithPosition.Add(new Character()
                    {
                        Char = chars[blobIndex],
                        Position = new Rectangle(blob.Rectangle.X + controlOffsetLeft + blob.Rectangle.Width / 2, blob.Rectangle.Y + controlOffsetTop + blob.Rectangle.Height / 2, blob.Rectangle.Width, blob.Rectangle.Height)
                    });
                    blobIndex++;
                }
                return charsWithPosition;
            }

        }

        private Rectangle AddPadding(Rectangle rectangle, int padding)
        {
            return new Rectangle(rectangle.X - padding, rectangle.Y - padding, rectangle.Width + 2 * padding, rectangle.Height + 2 * padding);
        }

        public Bitmap Binarize(Bitmap image, bool invert = true,int lowThreshold=0)
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

        public KeyValuePair<string, Rect>? GetFirstMatchingWordCoordinates(List<string> words, Bitmap image, int? YOffset = null)
        {
            if (YOffset.HasValue)
            {
                image = CropImage(image, new Rectangle(0, YOffset.Value, image.Width, image.Height - YOffset.Value));
            }

            using (var ocrPage = _ocrEngine.Process(image))
            {
                using (var iter = ocrPage.GetIterator())
                {
                    iter.Begin();
                    //stringWriter.WriteLine(iter.GetText(PageIteratorLevel.Word));
                    do
                    {
                        do
                        {
                            do
                            {
                                do
                                {
                                    //if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                    //{
                                    //    stringWriter.WriteLine("<BLOCK>");
                                    //}

                                    string ocrWord = iter.GetText(PageIteratorLevel.Word);

                                    if (ocrWord != null)
                                    {
                                        string matchingWord = words.FirstOrDefault(x => ocrWord.Contains(x));

                                        if (matchingWord != null)
                                        {
                                            iter.TryGetBoundingBox(PageIteratorLevel.Word, out Rect wordPosition);
                                            if (YOffset.HasValue)
                                            {
                                                wordPosition = new Rect(wordPosition.X1, wordPosition.Y1 + YOffset.Value, wordPosition.Width, wordPosition.Height);
                                            }
                                            return new KeyValuePair<string, Rect>(matchingWord, wordPosition);
                                        }
                                    }
                                    //stringWriter.Write(" ");

                                    //if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                    //{
                                    //    stringWriter.WriteLine();
                                    //}
                                } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                //if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                //{
                                //    stringWriter.WriteLine();
                                //}
                            } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                        } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                    } while (iter.Next(PageIteratorLevel.Block));
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
            Crop filter = new Crop(cropArea);
            // apply the filter
            return filter.Apply(img);

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