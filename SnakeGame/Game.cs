using SnakeGame.Fruits;
using SnakeGame.RadnomService;
using SnakeGame.SnakeData;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public Timer Timer { get; private set; }

        public GameField GameField { get; }

        public Game(int fieldX, int fieldY, IRandomService randomService)
        {
            this.randomService = randomService;

            isStarted = false;
            this.GameField = new GameField(fieldX, fieldY, "D:\\Programs\\SnakeGame\\SnakeGame\\bin\\gress.jpg");
            snake = new Snake((GameField.Location.X + GameField.Width) / 2, (GameField.Location.Y + GameField.Height) / 2);
            fruit = GenerateGameFruit();

            GameField.AddElement(snake.Head);
            GameField.AddElement(fruit);

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
            Timer.Enabled = false;

            var oldPoint = snake.Head.Location;

            if (snake.Direction == Keys.Down && GameField.IsOutOfDownSide(snake.Head))
            {
                snake.Head.Location = new Point(oldPoint.X, GameField.FIELD_HEIGHT - snake.Head.Height);
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
                snake.Head.Location = new Point(GameField.FIELD_WIDTH - snake.Head.Width, oldPoint.Y);
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

            if (CanSnakeEatFruit())
            {
                // TODO Увеличить кол-во очков
                // TODO Увеличть скорость
                // TODO Увеличить змейку
                // TODO Удалить фрукт
            }

            Timer.Enabled = true;
        }

        private bool CanSnakeEatFruit()
            => snake.Head.Contains(fruit);

        private FruitBase GenerateGameFruit()
            => randomService.GenerateFruitNotIn(0, 0, GameField.FIELD_WIDTH - ElementBase.WIDTH, GameField.FIELD_HEIGHT - ElementBase.HEIGHT, snake.Body);

        private Snake snake { get; set; }
        private bool isStarted { get; set; }
        private FruitBase fruit { get; set; }

        private readonly IRandomService randomService;
    }
}
