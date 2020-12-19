using System.Drawing;

namespace WS.Wscapes.DataTypes
{
    public class Character
    {
        public char Char { get; set; }

        public Rectangle Position { get; set; }
        public bool IsSelected { get; set; }
    }
}
