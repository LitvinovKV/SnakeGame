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
            onPause = false;
            this.GameField = new GameField(fieldX, fieldY, "D:\\Programs\\SnakeGame\\SnakeGame\\bin\\gress.jpg");
            score = 0;
            
            snake = new Snake(0, 0);
            GameField.AddElement(snake.Head);

            var middleLeftTopPoint = ((GameField.SIZE / ElementBase.SIZE) / 2) * ElementBase.SIZE;
            snake.Head.Location = new Point(middleLeftTopPoint, middleLeftTopPoint);

            fruit = GenerateGameFruit();
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

            if (onPause)
            {
                return;
            }

            snake.Direction = key;
        }

        public void SpaceButtonClick()
        {
            Debug.WriteLine("SPACE KEY CLICKED!");
            onPause = !onPause;
            Timer.Enabled = !onPause & isStarted;

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
            if ((snake.Direction == Keys.Down && GameField.IsOutOfDownSide(snake.Head))
                || (snake.Direction == Keys.Up && GameField.IsOutOfUpSide(snake.Head))
                || (snake.Direction == Keys.Left && GameField.IsOutOfLeftSide(snake.Head))
                || (snake.Direction == Keys.Right && GameField.IsOutOfRightSide(snake.Head)))
            {
                Timer.Enabled = false;
                MessageBox.Show("YOU LOOOSE! OUT OF DOWN!");
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

            snake.Move(oldPoint.X, oldPoint.Y);

            if (CanSnakeEatFruit())
            {
                // TODO Увеличить кол-во очков
                score++;

                // TODO Увеличть скорость

                // TODO Увеличить змейку
                var newSnakePart = snake.IncreaseBody();
                GameField.AddElement(newSnakePart);

                // TODO Удалить фрукт
                GameField.RemoveElement(fruit);
                fruit = GenerateGameFruit();
                GameField.AddElement(fruit);
            }
        }

        private bool CanSnakeEatFruit()
            => snake.Head.Include(fruit);

        private FruitBase GenerateGameFruit()
        {
            while(true)
            {
                var randFruit = randomService.GenerateFruit(0, 0, GameField.SIZE - ElementBase.SIZE, GameField.SIZE - ElementBase.SIZE);
                
                if (!snake.Head.Include(randFruit))
                {
                    return randFruit;
                }
            }
        }

        private Snake snake { get; set; }
        private bool isStarted { get; set; }
        private bool onPause { get; set; }
        private FruitBase fruit { get; set; }
        private int score { get; set; }

        private readonly IRandomService randomService;
    }
}
