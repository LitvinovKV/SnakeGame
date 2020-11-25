using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public abstract class SnakePart : Label
    {
        protected virtual String ImageTitle { get; set; }
        
        public SnakePart(int x, int y)
        {
            Location = new Point(x, y);
            Width = Game.FIELD_WEIGHT;
            Height = Game.FIELD_HEIGHT;
            BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
