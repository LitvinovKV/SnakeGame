using System.Drawing;

namespace SnakeGame
{
    public class Game
    {
        public int FieldWidth { get; private set; }
        public int FieldHeight { get; private set; }
        public Point FieldStartPos { get; private set; }

        public Game(Point fieldStartPos, int fieldWidth, int fieldHeight)
        {
            FieldStartPos = fieldStartPos;
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
        }
    }
}
