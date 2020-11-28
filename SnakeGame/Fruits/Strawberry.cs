using System.Drawing;

namespace SnakeGame.Fruits
{
    public class Strawberry : FruitBase
    {
        public Strawberry(int x, int y) : base(x, y)
        {
            BackColor = Color.Red;
        }

        protected override string ImagePath { get; set; }
    }
}
