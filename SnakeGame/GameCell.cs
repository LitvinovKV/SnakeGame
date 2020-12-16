using System.Drawing;

namespace SnakeGame
{
    public class GameCell
    {
        public static Brush EMPTY_CELL = Brushes.White;
        public static int SIZE { get; } = 20;
        public Brush Brush { get; set; }
        public Rectangle Rect { get; }
        public (int, int) Position { get; }

        public GameCell(int i, int j)
        {
            Position = (i, j);
            Brush = EMPTY_CELL;
            var location = new Point(j * SIZE, i * SIZE);
            var size = new Size(SIZE, SIZE);
            Rect = new Rectangle(location, size);
        }
    }
}
