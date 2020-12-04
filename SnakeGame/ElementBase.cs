using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class ElementBase : Label
    {
        public static readonly int SIZE = 20;

        public int I { get; set; }
        public int J { get; set; }
        public ElementType Type { get; set; }

        public ElementBase(int i, int j)
        {
            I = i;
            J = j;
            Type = ElementType.BaseField;
            BackColor = Color.White;
            Location = new Point(j * SIZE, i * SIZE);
            Width = SIZE;
            Height = SIZE;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void Move(int i, int j)
        {
            I = i;
            J = j;
        }

        public void UpdateColor()
        {
            switch (Type)
            {
                case ElementType.SnakeHead:
                    BackColor = Color.Coral;
                    break;
                case ElementType.SnakeBodyPart:
                    BackColor = Color.Aqua;
                    break;
                case ElementType.Apple:
                    BackColor = Color.Green;
                    break;
                case ElementType.Banana:
                    BackColor = Color.Yellow;
                    break;
                case ElementType.Strawberry:
                    BackColor = Color.Red;
                    break;
                case ElementType.BaseField:
                    BackColor = Color.White;
                    break;
            }
        }
    }
}
