using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class SnakeLayer : PictureBox
    {
        public Snake Snake { get; private set; }

        public SnakeLayer(PictureBoxCells parentLayer, int rowNumber, int columnNumber)
        {
            Parent = parentLayer;

            //without this + 1 left and bottom border hide
            Width = parentLayer.Width;
            Height = parentLayer.Height;
            BackColor = Color.Transparent;

            Snake = new Snake(rowNumber, columnNumber, PictureBoxCell.SIZE * PictureBoxCell.SIZE);
            pen = new Pen(Color.Black);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            var graphics = pe.Graphics;
            var rect = new Rectangle(
                Snake.Head.Item2 * PictureBoxCell.SIZE,
                Snake.Head.Item1 * PictureBoxCell.SIZE,
                PictureBoxCell.SIZE,
                PictureBoxCell.SIZE);

            graphics.DrawRectangle(pen, rect);
            graphics.FillRectangle(Snake.HEAD_BRUSH, rect);
        }

        public void UpdateLayer() => Invalidate();

        private Pen pen;
    }
}
