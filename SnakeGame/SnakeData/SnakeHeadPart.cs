using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeData
{
    public class SnakeHeadPart : SnakePart
    {
        protected override string ImageTitle { get; set; }

        public SnakeHeadPart(int x, int y) : base(x, y)
        {
            field.BackColor = Color.Blue;
        }
    }
}
