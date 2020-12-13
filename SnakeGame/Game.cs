using SnakeGame.RadnomService;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public GameLayer GameLayer { get; }

        public Game(int fieldX, int fieldY)
        {
            isStarted = false;
            onPause = false;
            currentSnakeDirection = Keys.None;

            GameLayer = new GameLayer(fieldX, fieldY);
            var middleIndex = GameLayer.COUNT_CELLS / 2;
            snake = new Snake(middleIndex, middleIndex);
            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;

            IncreaseSnake(middleIndex + 1, middleIndex);
            IncreaseSnake(middleIndex + 2, middleIndex);
            IncreaseSnake(middleIndex + 3, middleIndex);

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
            currentSnakeDirection =
                currentSnakeDirection == Keys.Up && clickedArrow == Keys.Down
                || currentSnakeDirection == Keys.Down && clickedArrow == Keys.Up
                || currentSnakeDirection == Keys.Left && clickedArrow == Keys.Right
                || currentSnakeDirection == Keys.Right && clickedArrow == Keys.Left
                ? currentSnakeDirection
                : clickedArrow;
            //currentSnakeDirection = clickedArrow; Если можно идти в противоположную сторону

            var newSnakeHeadPosition = snake.HeadIndexes;
            switch (currentSnakeDirection)
            {
                case Keys.Up:
                    newSnakeHeadPosition.Item1--;
                    break;
                case Keys.Down:
                    newSnakeHeadPosition.Item1++;
                    break;
                case Keys.Left:
                    newSnakeHeadPosition.Item2--;
                    break;
                case Keys.Right:
                    newSnakeHeadPosition.Item2++;
                    break;
            }

            if (CrashedIntoBorder(newSnakeHeadPosition.Item1, newSnakeHeadPosition.Item2))
            {
                MessageBox.Show($"YOU ARE CRASHED {currentSnakeDirection} BORDER!!!");
                return;
            }

            UpdateSnakeCells(newSnakeHeadPosition.Item1, newSnakeHeadPosition.Item2);

            //SnakeLayer.Snake.MoveTo(newSnakeHeadPosition.Item1, newSnakeHeadPosition.Item2);
            //SnakeLayer.UpdateLayer();
            //snakeLayer.MoveSnakeHeadTo(snakeHeadPosition.Item1, snakeHeadPosition.Item2);

            // TODO Попытаться придумать что-то с обновлением цвета ячейки по типу автоматически
            //if (currentSnakeLenght > 1)
            //{
            //    snakeBody[currentSnakeLenght - 1].Type = ElementType.BaseField;
            //    snakeBody[currentSnakeLenght - 1].UpdateColor();

            //    for (var i = currentSnakeLenght - 1; i >= 1; i--)
            //    {
            //        snakeBody[i] = snakeBody[i - 1];
            //        snakeBody[i].Type = ElementType.SnakeBodyPart;
            //        snakeBody[i].UpdateColor();
            //    }

            GameLayer.Refresh();
            timer.Enabled = true;
        }

        //GameField.UpdateFieldElementToBase(headI, headJ);
        //snakeHead = GameField.UpdateFieldElement(snakeHeadNewI, snakeHeadNewJ, SNAKE_HEAD_BRUSH);

        //snakeHead = GameField.UpdateTableField(snakeHead, snakeHeadNewI, snakeHeadNewJ);
        //snakeHead.Type = ElementType.SnakeHead;
        //snakeHead.UpdateColor();
        //snakeBody[0] = snakeHead;

        //    Timer.Enabled = true;
        //}

        private bool CrashedIntoBorder(int i, int j)
            => currentSnakeDirection == Keys.Up && i < 0
                || currentSnakeDirection == Keys.Down && i > GameLayer.COUNT_CELLS - 1
                || currentSnakeDirection == Keys.Left && j < 0
                || currentSnakeDirection == Keys.Right && j > GameLayer.COUNT_CELLS - 1;

        private void UpdateSnakeCells(int newSnakeHeadI, int newSnakeHeadJ)
        {
            if (snake.CurrentLength == 0)
            {
                GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = GameCell.EMPTY_CELL;
            }
            else
            {

                var lastBodyElement = snake.BodyIndexes[snake.CurrentLength - 1];
                GameLayer[lastBodyElement.Item1, lastBodyElement.Item2].Brush = GameCell.EMPTY_CELL;

                for (var i = snake.CurrentLength - 1; i > 0; i--)
                {
                    snake.BodyIndexes[i] = snake.BodyIndexes[i - 1];
                }

                snake.BodyIndexes[0] = snake.HeadIndexes;
                GameLayer[snake.BodyIndexes[0].Item1, snake.BodyIndexes[0].Item2].Brush = Snake.SNAKE_BODY_CELL;
            }

            snake.HeadIndexes = (newSnakeHeadI, newSnakeHeadJ);
            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;
        }

        private void IncreaseSnake(int i, int j)
        {
            snake.IncreaseBody(i, j);
            GameLayer[i, j].Brush = Snake.SNAKE_BODY_CELL;
        }

        private Timer timer;
        private bool isStarted;
        private bool onPause;
        private Keys clickedArrow;
        private Keys currentSnakeDirection;
        private Snake snake;

        private readonly IRandomService randomService;
    }
}
