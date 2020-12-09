using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameFieldLayer : PictureBox
    {
        public static int COUNT_CELLS { get; } = 27;
        public static int CELL_SIZE { get; } = 20;

        public Stack<RectangleWrapp> RedrawRectWrappStack { get; }

        public GameFieldLayer(int x, int y)
        {
            NewTable = new RectangleWrapp[COUNT_CELLS, COUNT_CELLS];

            for (var i = 0; i < COUNT_CELLS; i++)
            {
                for (var j = 0; j < COUNT_CELLS; j++)
                {
                    NewTable[i, j] = new RectangleWrapp(i, j);
                }
            }

            //without this + 1 left and bottom border hide
            Width = COUNT_CELLS * CELL_SIZE + 1;
            Height = COUNT_CELLS* CELL_SIZE + 1;

            Location = new Point(x, y);
        }

        public RectangleWrapp this[int i, int j]
        {
            get
            {
                CheckTableIndexes(i, j);
                return NewTable[i, j];
            }
        }

        public RectangleWrapp UpdateFieldElement(int i, int j, Brush brush)
        {
            CheckTableIndexes(i, j);
            RedrawRectWrappStack.Push(NewTable[i, j]);
            NewTable[i, j].CurrentBrush = brush;
            return NewTable[i, j];
        }

        public void UpdateFieldElementToBase(int i, int j)
            => UpdateFieldElement(i, j, Game.EMPTY_CELL);

        private void CheckTableIndexes(int i, int j)
        {
            if (i < 0 || i >= COUNT_CELLS)
            {
                throw new ArgumentException($"i must be >= 0 and < { COUNT_CELLS }");
            }

            if (j < 0 || j >= COUNT_CELLS)
            {
                throw new ArgumentException($"j must be >= 0 and < { COUNT_CELLS }");
            }
        }

        private void RedrawStackElements(object sender, PaintEventArgs pe)
        {
            var graphics = pe.Graphics;

            while (RedrawRectWrappStack.Count != 0)
            {
                var element = RedrawRectWrappStack.Pop();
                graphics.FillRectangle(element.CurrentBrush, element.Rect);
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
                    graphics.FillRectangle(NewTable[i, j].CurrentBrush, NewTable[i, j].Rect);
                    graphics.DrawRectangle(pen, NewTable[i, j].Rect);
                }
            }
        }

        private RectangleWrapp[,] NewTable { get; }
    }
}
