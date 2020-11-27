using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public abstract class ElementBase : Label
    {
        public static readonly int WIDTH = 20;
        public static readonly int HEIGHT = 20;

        protected abstract String ImagePath { get; set; }

        public ElementBase(int x, int y)
        {
            Location = new Point(x, y);
            Width = WIDTH;
            Height = HEIGHT;
            BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
