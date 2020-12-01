using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameField : PictureBox
    {
        public static readonly int SIZE = 27 * ElementBase.SIZE;

        public void AddElement(Control element)
            => Controls.Add(element);

        public void RemoveElement(Control element)
            => Controls.Remove(element);

        public bool IsOutOfLeftSide(Control element)
            => Contains(element) && element.Location.X <= 0;

        public bool IsOutOfUpSide(Control element)
            => Contains(element) && element.Location.Y <= 0;

        public bool IsOutOfRightSide(Control element)
            => Contains(element) && element.Location.X + element.Width >= SIZE;

        public bool IsOutOfDownSide(Control element)
            => Contains(element) && element.Location.Y + element.Height >= SIZE;

        public GameField(int x, int y, String backgroundImagePath)
        {
            Width = SIZE;
            Height = SIZE;
            Location = new Point(x, y);

            // While don't have images
            if (backgroundImagePath != null)
            {
                Image = Image.FromFile(backgroundImagePath);
            }
        }
    }
}
