using System.Drawing;

namespace SnakeGame.Fruits
{
    public class Apple : FruitBase
    {
        public Apple(int x, int y) : base(x, y)
        {
            BackColor = Color.Orange;
        }

        protected override string ImagePath { get; set; }
    }
}
