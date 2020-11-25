using System;
using System.Drawing;

namespace SnakeGame.SnakeData
{
    public class SnakeBodyPart : SnakePart
    {
        protected override String ImageTitle { get; set; }

        public SnakeBodyPart(int x, int y) : base(x, y)
        {
            BackColor = Color.Red;
        }
    }
}
