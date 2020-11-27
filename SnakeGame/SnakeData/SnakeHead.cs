using System.Drawing;

namespace SnakeGame.SnakeData
{
    public class SnakeHead : ElementBase
    {
        public SnakeHead(int x, int y) : base(x, y)
        {
            BackColor = Color.Blue;
        }

        protected override string ImagePath { get; set; }
    }
}
