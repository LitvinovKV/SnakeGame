using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame.SnakeData
{
    public class Snake
    {
        public static readonly int SPEED = ElementBase.SIZE;

        public Keys Direction { get; set; }
        public SnakeHead Head { get; private set; }
        public Stack<ElementBase> Body { get; }

        public Snake()
        {
            Body = new Stack<ElementBase>();
        }

        public Snake(int headX, int headY) : this()
        {
            Head = new SnakeHead(headX, headY);
        }

        public SnakeBodyPart IncreaseBody()
        {
            var headLocation = Head.Location;
            var newSnakePart = new SnakeBodyPart(headLocation.X, headLocation.Y);
            Body.Push(newSnakePart);

            switch (Direction)
            {
                case Keys.Up:
                    Head.Location = new Point(headLocation.X, headLocation.Y - ElementBase.SIZE);
                    break;
                case Keys.Down:
                    Head.Location = new Point(headLocation.X, headLocation.Y + ElementBase.SIZE);
                    break;
                case Keys.Left:
                    Head.Location = new Point(headLocation.X - ElementBase.SIZE, headLocation.Y);
                    break;
                case Keys.Right:
                    Head.Location = new Point(headLocation.X + ElementBase.SIZE, headLocation.Y);
                    break;
            }

            return newSnakePart;
        }

        public void Move(int x, int y)
        {
            var locationForPrevPart = Head.Location;
            Head.Location = new Point(x, y);

            foreach (var bodyPart in Body)
            {
                var oldLocation = bodyPart.Location;
                bodyPart.Location = locationForPrevPart;
                locationForPrevPart = oldLocation;
            }
        }
    }
}
