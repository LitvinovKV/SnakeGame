using SnakeGame.RadnomService;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public GameLayer GameLayer { get; }

        public Game(int fieldX, int fieldY, IRandomService randomService)
        {
            this.randomService = randomService;

            isStarted = false;
            onPause = false;

            GameLayer = new GameLayer(fieldX, fieldY);
            var middleIndex = GameLayer.COUNT_CELLS / 2;
            snake = new Snake(middleIndex, middleIndex);
            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;

            pointCell = GeneratePointCell();

            InitTimer();
        }

        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += MoveSnake;
        }

        public void ArrowButtonClick(Keys key)
        {
            clickedArrow = key;
            Debug.WriteLine("ARROW CLICKED: " + key.ToString());

            if (!isStarted)
            {
                StartGame();
            }

            if (onPause)
            {
                return;
            }
        }

        public void SpaceButtonClick()
        {
            Debug.WriteLine("SPACE KEY CLICKED!");
            onPause = !onPause;
            timer.Enabled = !onPause & isStarted;

            Debug.WriteLine("GAME " + (timer.Enabled ? "PAUSED" : "RESUMED"));
        }

        private void StartGame()
        {
            Debug.WriteLine("GAME STARTED");
            isStarted = true;
            timer.Enabled = true;
        }

        private void MoveSnake(object sender, EventArgs e)
        {
            timer.Enabled = false;

            // Если нельзя идти в противоположную сторону
            snake.Direction =
                snake.Direction == Keys.Up && clickedArrow == Keys.Down
                || snake.Direction == Keys.Down && clickedArrow == Keys.Up
                || snake.Direction == Keys.Left && clickedArrow == Keys.Right
                || snake.Direction == Keys.Right && clickedArrow == Keys.Left
                ? snake.Direction
                : clickedArrow;
            //currentSnakeDirection = clickedArrow; Если можно идти в противоположную сторону

            var newSnakeHeadPosition = GetNewSnakePosition(snake.Direction, snake.HeadIndexes);

            if (CrashedIntoBorder(newSnakeHeadPosition.Item1, newSnakeHeadPosition.Item2))
            {
                MessageBox.Show($"YOU ARE CRASHED {snake.Direction} BORDER!!!");
                return;
            }

            if (newSnakeHeadPosition == pointCell.Position)
            {
                snake.Increase(newSnakeHeadPosition);
                pointCell = GeneratePointCell();
            }
            else
            {
                var prevTailPosition = snake.Move(newSnakeHeadPosition);
                GameLayer[prevTailPosition.Item1, prevTailPosition.Item2].Brush = GameCell.EMPTY_CELL;
            }

            UpdateSnakeCells();

            GameLayer.Refresh();
            timer.Enabled = true;
        }
        
        private (int, int) GetNewSnakePosition(Keys snakeDirection, (int, int) snakeHeadPosition)
        {
            switch (snakeDirection)
            {
                case Keys.Up:
                    snakeHeadPosition.Item1--;
                    break;
                case Keys.Down:
                    snakeHeadPosition.Item1++;
                    break;
                case Keys.Left:
                    snakeHeadPosition.Item2--;
                    break;
                case Keys.Right:
                    snakeHeadPosition.Item2++;
                    break;
            }

            return snakeHeadPosition;
        }

        private bool CrashedIntoBorder(int i, int j)
            => snake.Direction == Keys.Up && i < 0
                || snake.Direction == Keys.Down && i > GameLayer.COUNT_CELLS - 1
                || snake.Direction == Keys.Left && j < 0
                || snake.Direction == Keys.Right && j > GameLayer.COUNT_CELLS - 1;

        private void UpdateSnakeCells()
        {   
            if (snake.CurrentLength > 1)
            {
                GameLayer[snake.BodyIndexes[1].Item1, snake.BodyIndexes[1].Item2].Brush = Snake.SNAKE_BODY_CELL;
            }

            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;
        }

        private GameCell GeneratePointCell()
        {
            var randomIndexes = randomService.GenerateIndexesExpect(GameLayer.GetTableIndexesLikeOnedimensionalArray(), snake.BodyIndexes);
            
            if (randomIndexes == null)
            {
                return null;
            }

            var gameCell = GameLayer[randomIndexes.Value.Item1, randomIndexes.Value.Item2];
            gameCell.Brush = Brushes.MediumSeaGreen;
            return gameCell;
        }

        private Timer timer;
        private bool isStarted;
        private bool onPause;
        private Keys clickedArrow;
        private Snake snake;
        private GameCell pointCell;

        private readonly IRandomService randomService;
    }
}
