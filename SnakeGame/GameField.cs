using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameField : PictureBox
    {
        public static readonly int FIELD_WIDTH = 540;
        public static readonly int FIELD_HEIGHT = 540;

        public void AddElement(Control element)
            => Controls.Add(element);

        public void RemoveElement(Control element)
            => Controls.Remove(element);

        public bool IsOutOfLeftSide(Control element)
            => Contains(element) && element.Location.X <= 0;

        public bool IsOutOfUpSide(Control element)
            => Contains(element) && element.Location.Y <= 0;

        public bool IsOutOfRightSide(Control element)
            => Contains(element) && element.Location.X + element.Width >= FIELD_WIDTH;

        public bool IsOutOfDownSide(Control element)
            => Contains(element) && element.Location.Y + element.Height >= FIELD_HEIGHT;

        public GameField(int x, int y, String backgroundImagePath)
        {
            Width = FIELD_WIDTH;
            Height = FIELD_HEIGHT;
            Location = new Point(x, y);

            // While don't have images
            if (backgroundImagePath != null)
            {
                Image = Image.FromFile(backgroundImagePath);
            }
        }
    }
}
