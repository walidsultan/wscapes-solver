using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Wscapes.DataTypes;

namespace WS.Wscapes
{
    public class AppStore
    {
        public static Bitmap OriginalScreenshot { get; set; }
        public static Bitmap BinarizedScreenshot { get; set; }
        public static GameState CurrentGameState { get; set; }
        public static GameState PreviousGameState { get; set; }
        public static Point ClickPosition { get; set; }
        public static IEnumerable<Character> LevelControls { get; set; }
    }


}
