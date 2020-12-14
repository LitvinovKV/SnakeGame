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
        public static Brush SNAKE_BODY_CELL = Brushes.LightGreen;

        public ValueTuple<int, int> HeadIndexes 
        {
            get => BodyIndexes[0];
            set
            {
                if (!GameLayer.FieldContains(value))
                {
                    throw new ArgumentException($"incorrect indexes ({value.Item1}, {value.Item2}). Must be >= 0 and < {GameLayer.COUNT_CELLS}");
                }

                BodyIndexes[0] = value;
            }
        }
        public  ValueTuple<int, int>[] BodyIndexes { get; set; }
        public int CurrentLength { get; set; }

        public Snake(int i, int j)
        { 
            BodyIndexes = new ValueTuple<int, int>[GameLayer.COUNT_CELLS * GameLayer.COUNT_CELLS];
            HeadIndexes = (i, j);
            CurrentLength = 1;
        }

        public void IncreaseBody(int i, int j)
        {
            BodyIndexes[CurrentLength] = (i, j);
            CurrentLength++;
        }

        public void Move()
        {
            if (CurrentLength <= 1)
            {
                return;
            }

            for (var i = CurrentLength - 1; i > 1; i--)
            {
                BodyIndexes[i] = BodyIndexes[i - 1];
            }

            BodyIndexes[1] = HeadIndexes;
        }
    }
}
