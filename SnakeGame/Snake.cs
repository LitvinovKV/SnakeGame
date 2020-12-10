using System;
using System.Drawing;

namespace SnakeGame
{
    public class Snake
    {
        public static Brush HEAD_BRUSH = Brushes.DarkGreen;
        public static Brush SNAKE_BODY_BRUSH = Brushes.Goldenrod;

        public ValueTuple<int, int> Head { get; private set; }
        public ValueTuple<int, int>[] Body { get; }
        public int CurrentLength { get; set; }

        public Snake(int i, int j, int capacity)
        {
            Head = (i, j);
            Body = new ValueTuple<int, int>[capacity];
            CurrentLength = 0;
        }

        public void MoveTo(int i, int j)
        {
            Head = (i, j);
        }
    }
}
