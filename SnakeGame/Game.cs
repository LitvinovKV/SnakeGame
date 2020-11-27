using SnakeGame.SnakeData;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public static readonly int FIELD_WEIGHT = 20;
        public static readonly int FIELD_HEIGHT = 20;

        public Timer Timer { get; private set; }

        public GameField GameField { get; }

        public Game(int fieldX, int fieldY)
        {
            isStarted = false;
            this.GameField = new GameField(fieldX, fieldY);
            snake = new Snake((GameField.Field.Location.X + GameField.Field.Width) / 2, (GameField.Field.Location.Y + GameField.Field.Height) / 2);

            GameField.AddElement(snake.Head);

            InitTimer();
        }

        private void InitTimer()
        {
            Timer = new Timer();
            Timer.Interval = 100;
            Timer.Tick += MoveSnake;
        }

        public void ArrowButtonClick(Keys key)
        {
            Debug.WriteLine("ARROW CLICKED: " + key.ToString());

            if (!isStarted)
            {
                StartGame();
            }

            if (!Timer.Enabled)
            {
                return;
            }

            snake.Direction = key;
        }

        public void SpaceButtonClick()
        {
            Debug.WriteLine("SPACE KEY CLICKED!");

            Timer.Enabled = !Timer.Enabled & isStarted;
            
            Debug.WriteLine("GAME " + (Timer.Enabled ? "PAUSED" : "RESUMED"));
        }
        
        private void StartGame()
        {
            Debug.WriteLine("GAME STARTED");
            isStarted = true;
            Timer.Enabled = true;
        }

        private void MoveSnake(object sender, EventArgs e)
        {
            var oldPoint = snake.Head.Location;
         
            if (snake.Direction == Keys.Down && GameField.IsOutOfDownSide(snake.Head))
            {
                snake.Head.Location = new Point(oldPoint.X, GameField.FIELD_HEIGHT - FIELD_HEIGHT);
                return;
            }

            if (snake.Direction == Keys.Up && GameField.IsOutOfUpSide(snake.Head))
            {
                snake.Head.Location = new Point(oldPoint.X, 0);
                return;
            }

            if (snake.Direction == Keys.Left && GameField.IsOutOfLeftSide(snake.Head))
            {
                snake.Head.Location = new Point(0, oldPoint.Y);
                return;
            }

            if (snake.Direction == Keys.Right && GameField.IsOutOfRightSide(snake.Head))
            {
                snake.Head.Location = new Point(GameField.FIELD_WIDTH - FIELD_WEIGHT, oldPoint.Y);
                return;
            }

            switch (snake.Direction)
            {
                case Keys.Up:
                    oldPoint.Y -= Snake.SPEED;
                    break;
                case Keys.Down:
                    oldPoint.Y += Snake.SPEED;
                    break;
                case Keys.Left:
                    oldPoint.X -= Snake.SPEED;
                    break;
                case Keys.Right:
                    oldPoint.X += Snake.SPEED;
                    break;
            }

            snake.Head.Location = new Point(oldPoint.X, oldPoint.Y);
        }

        private Snake snake { get; set; }
        private bool isStarted { get; set; }
    }
}
