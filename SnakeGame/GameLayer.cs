using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace SnakeGame
{
    public class GameLayer : PictureBox
    {
        public static int COUNT_CELLS { get; } = 27;
        
        public GameLayer(int x, int y)
        {
            Table = new GameCell[COUNT_CELLS][];
            for (var i = 0; i < COUNT_CELLS; i++)
            {
                Table[i] = new GameCell[COUNT_CELLS];
                for (var j = 0; j < COUNT_CELLS; j++)
                {
                    Table[i][j] = new GameCell(i, j);
                }
            }

            //without this + 1 left and bottom border hide
            Width = COUNT_CELLS * GameCell.SIZE + 1;
            Height = COUNT_CELLS* GameCell.SIZE + 1;

            Location = new Point(x, y);
        }

        public GameCell this[int i, int j]
        {
            get
            {
                if (!FieldContains((i, j)))
                {
                    throw new ArgumentException($"incorrect indexes ({i}, {j}). Must be >= 0 and < {COUNT_CELLS}");
                }

                return Table[i][j];
            }
        }

        public (int, int)[] GetTableIndexesLikeOnedimensionalArray()
            => Table.SelectMany(arr => arr).Select(element => element.Position).ToArray();

        public static bool FieldContains((int, int) indexes)
        {
            if ((indexes.Item1 < 0 || indexes.Item1 >= COUNT_CELLS)
                || (indexes.Item2 < 0 || indexes.Item2 >= COUNT_CELLS))
            {
                return false;
            }

            return true;
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
                    graphics.FillRectangle(Table[i][j].Brush, Table[i][j].Rect);
                    graphics.DrawRectangle(pen, Table[i][j].Rect);
                }
            }
        }

        private GameCell[][] Table;
    }
}
