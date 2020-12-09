using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class SnakeLayer : PictureBox
    {
        public static Brush SNAKE_HEAD_BRUSH = Brushes.DarkGreen;
        public static Brush SNAKE_BODY_BRUSH = Brushes.Goldenrod;

        public SnakeLayer(PictureBox parentLayer)
        {
            //without this + 1 left and bottom border hide
            Width = parentLayer.Width;
            Height = parentLayer.Height;
            BackColor = Color.Transparent;
            Parent = parentLayer;
            SnakeBody = new List<Rectangle>();
        }

        public void MoveSnakeHeadTo(int i, int j)
        {
            snakeHeadI = i;
            snakeHeadJ = j;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            var pen = new Pen(Color.Black);

            var graphics = pe.Graphics;
            var rect = new Rectangle(
                snakeHeadJ * GameFieldLayer.CELL_SIZE,
                snakeHeadI * GameFieldLayer.CELL_SIZE,
                GameFieldLayer.CELL_SIZE,
                GameFieldLayer.CELL_SIZE);

            graphics.DrawRectangle(pen, rect);
            graphics.FillRectangle(SNAKE_HEAD_BRUSH, rect);
        }

        public int snakeHeadI;
        public int snakeHeadJ;
        private IList<Rectangle> SnakeBody;
    }
}
