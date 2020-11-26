using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public class Snake
    {
        public Snake()
        {
            body = new List<SnakePart>();
        }

        public Snake(int headX, int headY) : this()
        {
            body.Add(new SnakeHeadPart(headX, headY));
        }

        public List<SnakePart> body { get; private set; }
        public Keys Direction { get; set; }
    }
}
