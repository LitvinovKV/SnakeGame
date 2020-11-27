using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public class Snake
    {
        public static readonly int SPEED = 10;

        public Keys Direction { get; set; }
        public SnakeHead Head { get; private set; }

        public Snake()
        {
            Body = new Stack<ElementBase>();
        }

        public Snake(int headX, int headY) : this()
        {
            Head = new SnakeHead(headX, headY);
            Body.Push(Head);
        }

        private Stack<ElementBase> Body { get; set; }
    }
}
