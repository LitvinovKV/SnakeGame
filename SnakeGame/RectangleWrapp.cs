using System.Drawing;

namespace SnakeGame
{
    public class RectangleWrapp
    {
        public static int SIZE = 20;

        public Brush CurrentBrush { get; set; }
        public Rectangle Rect { get; set; }

        public RectangleWrapp(int i, int j)
        {
            CurrentBrush = Game.EMPTY_CELL;
            var location = new Point(j * SIZE, i * SIZE);
            var size = new Size(SIZE, SIZE);
            Rect = new Rectangle(location, size);
        }
    }
}
