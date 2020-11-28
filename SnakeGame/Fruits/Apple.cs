using System.Drawing;

namespace SnakeGame.Fruits
{
    public class Apple : FruitBase
    {
        public Apple(int x, int y) : base(x, y)
        {
            BackColor = Color.Red;
        }

        protected override string ImagePath { get; set; }
    }
}
