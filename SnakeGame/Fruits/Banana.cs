using System.Drawing;

namespace SnakeGame.Fruits
{
    public class Banana : FruitBase
    {
        public Banana(int x, int y) : base(x, y)
        {
            BackColor = Color.Yellow;
        }

        protected override string ImagePath { get; set; }
    }
}
