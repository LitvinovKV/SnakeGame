using System.Drawing;

namespace SnakeGame.SnakeData
{
    public class SnakeHead : ElementBase
    {
        public SnakeHead(int x, int y) : base(x, y)
        {
            BackColor = Color.Black;
        }

        protected override string ImagePath { get; set; }
    }
}
