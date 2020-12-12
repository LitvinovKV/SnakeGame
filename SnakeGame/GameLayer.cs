using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameLayer : PictureBox
    {
        public static int COUNT_CELLS { get; } = 27;
        
        public GameLayer(int x, int y)
        {
            Table = new GameCell[COUNT_CELLS, COUNT_CELLS];
            for (var i = 0; i < COUNT_CELLS; i++)
            {
                for (var j = 0; j < COUNT_CELLS; j++)
                {
                    Table[i, j] = new GameCell(i, j);
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
                if (i < 0 || i >= COUNT_CELLS)
                {
                    throw new ArgumentException($"i must be >= 0 and < {COUNT_CELLS}");
                }
                
                if (j < 0 || j >= COUNT_CELLS)
                {
                    throw new ArgumentException($"j must be >= 0 and < {COUNT_CELLS}");
                }

                return Table[i, j];
            }
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

        private GameCell[,] Table;
    }

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

    public class Snake
    {
        public static Brush SNAKE_HEAD_CELL = Brushes.DarkKhaki;
        public static Brush SNAKE_BODY_CELL = Brushes.Honeydew;

        public ValueTuple<int, int> HeadIndexes { get; set; }
        public IList<ValueTuple<int, int>> BodyIndexes { get; set; }

        public Snake(int i, int j)
        {
            HeadIndexes = (i, j);
            BodyIndexes = new List<ValueTuple<int, int>>();
        }
    }
}
