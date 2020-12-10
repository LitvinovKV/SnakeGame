using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class PictureBoxCells : PictureBox
    {
        public static int COUNT_CELLS { get; } = 27;

        public Stack<PictureBoxCell> RedrawRectWrappStack { get; }

        public PictureBoxCells(int x, int y)
        {
            Table = new PictureBoxCell[COUNT_CELLS, COUNT_CELLS];

            for (var i = 0; i < COUNT_CELLS; i++)
            {
                for (var j = 0; j < COUNT_CELLS; j++)
                {
                    Table[i, j] = new PictureBoxCell(i, j);
                }
            }

            //without this + 1 left and bottom border hide
            Width = COUNT_CELLS * PictureBoxCell.SIZE + 1;
            Height = COUNT_CELLS* PictureBoxCell.SIZE + 1;

            Location = new Point(x, y);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graphics = pe.Graphics;
            var pen = new Pen(Color.Black);

            for (var i = 0; i < COUNT_CELLS; i++)
            {
                for (var j = 0; j < COUNT_CELLS; j++)
                {
                    graphics.FillRectangle(Table[i, j].Brush, Table[i, j].Rect);
                    graphics.DrawRectangle(pen, Table[i, j].Rect);
                }
            }
        }

        private PictureBoxCell[,] Table { get; }
    }
}
