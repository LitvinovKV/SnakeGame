using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeData
{
    public class Snake
    {
        public Snake(int headX, int headY)
        {
            head = new SnakeHeadPart(headX, headY);
        }

        public SnakeBodyPart head { get; private set; }
        public List<SnakeBodyPart> body { get; private set; }
    }
}
