using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameField
    {
        public static readonly int FIELD_WIDTH = 529;
        public static readonly int FIELD_HEIGHT = 537;

        public PictureBox Field { get; private set; }

        public void AddElement(Control element)
            => Field.Controls.Add(element);

        public bool IsOutOfLeftSide(Control element)
            => Field.Contains(element) && element.Location.X <= 0;

        public bool IsOutOfUpSide(Control element)
            => Field.Contains(element) && element.Location.Y <= 0;

        public bool IsOutOfRightSide(Control element)
            => Field.Contains(element) && element.Location.X + Game.FIELD_WEIGHT >= FIELD_WIDTH;

        public bool IsOutOfDownSide(Control element)
            => Field.Contains(element) && element.Location.Y + Game.FIELD_HEIGHT >= FIELD_HEIGHT;

        public GameField(int x, int y)
        {
            backgroundImagePath = "D:\\Programs\\SnakeGame\\SnakeGame\\bin\\gress.jpg";

            Field = new PictureBox()
            {
                Width = FIELD_WIDTH,
                Height = FIELD_HEIGHT,
                Location = new Point(x, y),
                BackColor = Color.Red,
                Image = Image.FromFile(backgroundImagePath)
            };
        }

        private String backgroundImagePath { get; }
    }
}
