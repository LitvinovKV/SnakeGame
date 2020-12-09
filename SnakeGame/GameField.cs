using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameField : Control
    {
        public static int SIZE { get; } = 27;
        public static int ELEMENT_SIZE { get; } = 20;

        public Stack<RectangleWrapp> RedrawRectWrappStack { get; }

        public GameField(int x, int y)
        {
            Enabled = false;
            NewTable = new RectangleWrapp[SIZE, SIZE];
            RedrawRectWrappStack = new Stack<RectangleWrapp>();

            for (var i = 0; i < SIZE; i++)
            {
                for (var j = 0; j < SIZE; j++)
                {
                    NewTable[i, j] = new RectangleWrapp(i, j);
                }
            }

            //without this + 1 left and bottom border hide
            Width = SIZE * ELEMENT_SIZE + 1;
            Height = SIZE* ELEMENT_SIZE + 1;

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
            if (i < 0 || i >= SIZE)
            {
                throw new ArgumentException($"i must be >= 0 and < { SIZE }");
            }

            if (j < 0 || j >= SIZE)
            {
                throw new ArgumentException($"j must be >= 0 and < { SIZE }");
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graphics = pe.Graphics;
            var pen = new Pen(Color.Black);

            for (var i = 0; i < SIZE; i++)
            {
                for (var j = 0; j < SIZE; j++)
                {
                    graphics.FillRectangle(NewTable[i, j].CurrentBrush, NewTable[i, j].Rect);
                    graphics.DrawRectangle(pen, NewTable[i, j].Rect);
                }
            }
        }

        private RectangleWrapp[,] NewTable { get; }
    }
}
