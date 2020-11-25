using System.Drawing;

namespace SnakeGame.SnakeData
{
    public class SnakeHeadPart : SnakeBodyPart
    {
        protected override string ImageTitle { get; set; }

        public SnakeHeadPart(int x, int y) : base(x, y)
        {
            BackColor = Color.Blue;
        }
    }
}
