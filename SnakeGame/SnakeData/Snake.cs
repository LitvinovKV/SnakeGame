using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public class Snake
    {
        public static readonly int SPEED = 10;

        public Keys Direction { get; set; }
        public SnakeHeadPart Head { get; private set; }

        public Snake()
        {
            Body = new Stack<SnakePart>();
        }

        public Snake(int headX, int headY) : this()
        {
            Head = new SnakeHeadPart(headX, headY);
            Body.Push(Head);
        }

        private Stack<SnakePart> Body { get; set; }
    }
}
