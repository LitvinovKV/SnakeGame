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

        private SnakePart head { get; set; }
        private List<SnakePart> body { get; set; }
    }
}
