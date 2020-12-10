using System.Drawing;

namespace SnakeGame
{
    public class PictureBoxCell
    {
        public static int SIZE = 20;

        public Brush Brush { get; set; }
        public Rectangle Rect { get; set; }

        public PictureBoxCell(int i, int j, Brush brush = null)
        {
            Brush = brush ?? Brushes.White;
            var location = new Point(j * SIZE, i * SIZE);
            var size = new Size(SIZE, SIZE);
            Rect = new Rectangle(location, size);
        }
    }
}
