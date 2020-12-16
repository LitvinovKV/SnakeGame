using SnakeGame.RadnomService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public GameLayer GameLayer { get; }

        public Game(int fieldX, int fieldY, IRandomService randomService, Label scoreLabel, Label maxSessionScoreLabel, Label levelLabel)
        {
            this.randomService = randomService;
            this.scoreLabel = scoreLabel;
            this.maxSessionScoreLabel = maxSessionScoreLabel;
            this.levelLabel = levelLabel;

            isStarted = false;
            onPause = false;

            score = 0;
            maxSessionScore = 0;
            level = 1;

            defaultTimerInterval = 100;
            timerIntervalDecrease = 5;

            GameLayer = new GameLayer(fieldX, fieldY);
            startSnakeHeadPosition = (GameLayer.COUNT_CELLS / 2, GameLayer.COUNT_CELLS / 2);
            snake = new Snake(startSnakeHeadPosition);
            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;
            pointCell = GeneratePointCell();

            InitTimer();
        }

        private void InitGameField()
        {
            snake.Reset(startSnakeHeadPosition);
            GameLayer[snake.HeadIndexes.Item1, snake.HeadIndexes.Item2].Brush = Snake.SNAKE_HEAD_CELL;
            pointCell = GeneratePointCell();

            if (maxSessionScore < score)
            {
                maxSessionScore = score;
                maxSessionScoreLabel.Text = maxSessionScore.ToString();
            }

            score = 0;
            scoreLabel.Text = "0";

            level = 1;
            levelLabel.Text = "1";

            timer.Interval = defaultTimerInterval;
        }

        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = defaultTimerInterval;
            timer.Tick += MoveSnake;
        }

        public void ArrowButtonClick(Keys key)
        {
            clickedArrow = key;

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
            onPause = !onPause;
            timer.Enabled = !onPause & isStarted;
        }

        private void StartGame()
        {
            isStarted = true;
            timer.Enabled = true;
        }

        private void MoveSnake(object sender, EventArgs e)
        {
            timer.Enabled = false;

            
            snake.Direction = UpdateDirection();

            var newSnakeHeadPosition = GetNewSnakePosition(snake.Direction, snake.HeadIndexes);

            if (CrashedIntoBorder(newSnakeHeadPosition.Item1, newSnakeHeadPosition.Item2))
            {
                LoseGame("OOPS! YOU ARE EATING YOURSELF!!!");
                return;
            }

            var prevTailPosition = snake.Move(newSnakeHeadPosition);
            if (newSnakeHeadPosition == pointCell.Position)
            {
                snake.Increase(prevTailPosition);
                score++;
                scoreLabel.Text = score.ToString();
                pointCell = GeneratePointCell();
                LevelUp();
            }
            else
            {
                GameLayer[prevTailPosition.Item1, prevTailPosition.Item2].Brush = GameCell.EMPTY_CELL;
            }

            UpdateSnakeCells();

            GameLayer.Refresh();
            
            if (snake.IsEatSelf())
            {
                LoseGame("OOPS! YOU ARE EATING YOURSELF!!!");
                return;
            }

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


        private void LoseGame(String text)
        {
            isStarted = false;
            MessageBox.Show(text + $" YOUR SCORE = {score}");

            pointCell.Brush = GameCell.EMPTY_CELL;
            for (var i = 0; i < snake.CurrentLength; i++)
            {
                GameLayer[snake.BodyIndexes[i].Item1, snake.BodyIndexes[i].Item2].Brush = GameCell.EMPTY_CELL;
            }

            InitGameField();
            GameLayer.Refresh();
        }

        private Keys UpdateDirection()
            // Если нельзя идти в противоположную сторону
            => snake.Direction == Keys.Up && clickedArrow == Keys.Down
            || snake.Direction == Keys.Down && clickedArrow == Keys.Up
            || snake.Direction == Keys.Left && clickedArrow == Keys.Right
            || snake.Direction == Keys.Right && clickedArrow == Keys.Left
                ? snake.Direction
                : clickedArrow;
        //currentSnakeDirection = clickedArrow; Если можно идти в противоположную сторону

        private void LevelUp()
        {
            if (score % 5 == 0)
            {
                level++;
                levelLabel.Text = level.ToString();
                timer.Interval -= timerIntervalDecrease;
            }
        }

        private Timer timer;
        private bool isStarted;
        private bool onPause;
        private Keys clickedArrow;
        private Snake snake;
        private GameCell pointCell;
        private (int, int) startSnakeHeadPosition;
        private int score;
        private int maxSessionScore;
        private Label scoreLabel;
        private Label maxSessionScoreLabel;
        private Label levelLabel;
        private int timerIntervalDecrease;
        private int defaultTimerInterval;
        private int level;

        private readonly IRandomService randomService;
    }
}