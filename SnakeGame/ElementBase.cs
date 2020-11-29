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

        public bool Include(Control otherElement)
            => Include(otherElement.Location)
                || Include(new Point(otherElement.Location.X + otherElement.Width, otherElement.Location.Y + otherElement.Height))
                || Include(new Point(otherElement.Location.X + otherElement.Width, otherElement.Location.Y))
                || Include(new Point(otherElement.Location.X, otherElement.Location.Y + otherElement.Height));

        public bool Include(Point otherPoint)
            => otherPoint.X >= Location.X && otherPoint.X <= Location.X + Width
                && otherPoint.Y >= Location.Y && otherPoint.Y <= Location.Y + Height;
    }
}
