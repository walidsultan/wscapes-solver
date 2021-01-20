using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Wscapes
{
    public class Dimensions
    {
        public int OcrInitWordLeft { get; set; }
        public int OcrInitWordTop { get; set; }
        public int OcrInitWordWidth { get; set; }
        public int OcrInitWordHeight { get; set; }

        public int OcrControlsLeft { get; set; }
        public int OcrControlsTop { get; set; }
        public int OcrControlsWidth { get; set; }
        public int OcrControlsHeight { get; set; }
        public int OcrControlsPadding { get; set; }
        public int OcrControlsMinArea { get; set; }
        public int OcrControlsMaxArea { get; set; }
        public int OcrControlsMinHeight { get; set; }
        public int OcrControlsMaxHeight { get; set; }

        public int OcrContinueWordLeft { get; set; }
        public int OcrContinueWordTop { get; set; }
        public int OcrContinueWordWidth { get; set; }
        public int OcrContinueWordHeight { get; set; }

        public double NativeCooridinatesXFactor { get; set; }
        public double NativeCooridinatesYFactor { get; set; }

        public int ReOrderLevelX { get; set; }
        public int ReOrderLevelY { get; set; }

        public int IsFourWordsLeft { get; set; }
        public int IsFourWordsTop { get; set; }
        public int IsFourWordsWidth { get; set; }
        public int IsFourWordsHeight { get; set; }

        public int PiggyBankLeft { get; set; }
        public int PiggyBankTop { get; set; }
        public int PiggyBankWidth { get; set; }
        public int PiggyBankHeight { get; set; }
        public int PiggyBankCrossOffsetX { get; set; }
        public int PiggyBankCrossOffsetY { get; set; }

        private const double PHONE_ORIGINAL_RESOLUTION_H = 1440;  //Pixel xl horizontal resolution
        private const double PHONE_ORIGINAL_RESOLUTION_V = 2560; //Pixel xl vertical resolution


        public Dimensions(int screenWidth, int screenHeight)
        {
            OcrInitWordLeft = (int)(Constants.OCR_PCT_INIT_WORD_LEFT * screenWidth);
            OcrInitWordTop = (int)(Constants.OCR_PCT_INIT_WORD_TOP * screenHeight);
            OcrInitWordWidth = (int)(Constants.OCR_PCT_INIT_WORD_WIDTH * screenWidth);
            OcrInitWordHeight = (int)(Constants.OCR_PCT_INIT_WORD_HEIGHT * screenHeight);

            OcrControlsLeft = (int)(Constants.OCR_PCT_CONTROLS_LEFT * screenWidth);
            OcrControlsTop = (int)(Constants.OCR_PCT_CONTROLS_TOP * screenHeight);
            OcrControlsWidth = (int)(Constants.OCR_PCT_CONTROLS_WIDTH * screenWidth);
            OcrControlsHeight = (int)(Constants.OCR_PCT_CONTROLS_HEIGHT * screenHeight);
            OcrControlsPadding = (int)(Constants.OCR_PCT_CONTROLS_PADDING * screenWidth);

            OcrControlsMinArea = (int)(Constants.OCR_PCT_CONTROLS_MIN_AREA * screenWidth * screenHeight);
            OcrControlsMaxArea = (int)(Constants.OCR_PCT_CONTROLS_MAX_AREA * screenWidth * screenHeight);
            OcrControlsMinHeight = (int)(Constants.OCR_PCT_CONTROLS_MIN_HEIGHT * screenHeight);
            OcrControlsMaxHeight = (int)(Constants.OCR_PCT_CONTROLS_MAX_HEIGHT * screenHeight);

            OcrContinueWordLeft = (int)(Constants.OCR_PCT_CONTINUE_WORD_LEFT * screenWidth);
            OcrContinueWordTop = (int)(Constants.OCR_PCT_CONTINUE_WORD_TOP * screenHeight);
            OcrContinueWordWidth = (int)(Constants.OCR_PCT_CONTINUE_WORD_WIDTH * screenWidth);
            OcrContinueWordHeight = (int)(Constants.OCR_PCT_CONTINUE_WORD_HEIGHT * screenHeight);

            NativeCooridinatesXFactor = PHONE_ORIGINAL_RESOLUTION_H / screenWidth;
            NativeCooridinatesYFactor = PHONE_ORIGINAL_RESOLUTION_V / screenHeight;

            ReOrderLevelX = (int)(Constants.PCT_REORDER_LEFT * screenWidth);
            ReOrderLevelY = (int)(Constants.PCT_REORDER_TOP * screenHeight);

            IsFourWordsLeft = (int)(Constants.PCT_IS_FOUR_WORDS_LEFT * screenWidth);
            IsFourWordsTop = (int)(Constants.PCT_IS_FOUR_WORDS_TOP * screenHeight);
            IsFourWordsWidth = (int)(Constants.PCT_IS_FOUR_WORDS_WIDTH * screenWidth);
            IsFourWordsHeight = (int)(Constants.PCT_IS_FOUR_WORDS_HEIGHT * screenHeight);

            PiggyBankLeft = (int)(Constants.PCT_PIGGY_BANK_LEFT * screenWidth);
            PiggyBankTop = (int)(Constants.PCT_PIGGY_BANK_TOP * screenHeight);
            PiggyBankWidth = (int)(Constants.PCT_PIGGY_BANK_WIDTH * screenWidth);
            PiggyBankHeight = (int)(Constants.PCT_PIGGY_BANK_HEIGHT * screenHeight);
            PiggyBankCrossOffsetX = (int)(Constants.PCT_PIGGY_BANK_CROSS_OFFSET_X * screenWidth);
            PiggyBankCrossOffsetY = (int)(Constants.PCT_PIGGY_BANK_CROSS_OFFSET_Y * screenHeight);
        }


    }
}
