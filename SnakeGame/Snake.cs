using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SnakeGame
{
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
        public ValueTuple<int, int>[] BodyIndexes { get; set; }
        public int CurrentLength { get; set; }
        public Keys Direction { get; set; }

        public Snake((int, int) position)
        {
            BodyIndexes = new ValueTuple<int, int>[GameLayer.COUNT_CELLS * GameLayer.COUNT_CELLS];
            HeadIndexes = position;
            CurrentLength = 1;
            Direction = Keys.None;
        }

        public (int, int) Move((int, int) newHeadPosition)
        {
            var shouldClearCellPosition = BodyIndexes[CurrentLength - 1];

            if (CurrentLength > 1)
            {
                for (var i = CurrentLength - 1; i > 1; i--)
                {
                    BodyIndexes[i] = BodyIndexes[i - 1];
                }

                BodyIndexes[1] = HeadIndexes;
            }

            BodyIndexes[0] = (newHeadPosition.Item1, newHeadPosition.Item2);

            return shouldClearCellPosition;
        }

        public bool IsEatSelf()
            => BodyIndexes.Skip(1).Any(bodyElement => bodyElement == BodyIndexes[0]);

        public void Increase((int, int) newElementPosition)
        {
            BodyIndexes[CurrentLength] = newElementPosition;
            CurrentLength++;
        }

        public void Reset((int, int) headPosition)
        {
            HeadIndexes = headPosition;
            Direction = Keys.None;
            CurrentLength = 1;
        }
    }
}
