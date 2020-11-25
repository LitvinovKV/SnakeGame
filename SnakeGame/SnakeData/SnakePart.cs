using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public class SnakePart
    {
        public static readonly int FIELD_WEIGHT = 20;
        public static int FIELD_HEIGHT = 20;
     
        protected virtual String ImageTitle { get; set; }
        protected Label field { get; set; }

        public SnakePart(int x, int y)
        {
            field = new Label()
            {
                Location = new Point(x, y),
                BackColor = Color.Red,
                Width = FIELD_WEIGHT,
                Height = FIELD_HEIGHT,
                BorderStyle = BorderStyle.FixedSingle
            };
        }
    }
}
