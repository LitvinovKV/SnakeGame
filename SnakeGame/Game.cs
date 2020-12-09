using SnakeGame.RadnomService;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Game
    {
        public static Brush SNAKE_HEAD_BRUSH { get; } = Brushes.Aqua;
        public static Brush SNAKE_BODY_BRUSH { get; } = Brushes.AliceBlue;
        public static Brush EMPTY_CELL { get; } = Brushes.White;

        public Timer Timer { get; private set; }

        public GameFieldLayer GameFieldLayer { get; }
        public SnakeLayer SnakeLayer { get; }

        public Game(int fieldX, int fieldY)
        {
            isStarted = false;
            onPause = false;
            currentSnakeDirection = Keys.None;

            GameFieldLayer = new GameFieldLayer(fieldX, fieldY);
            SnakeLayer = new SnakeLayer(GameFieldLayer);

            var GameFieldLayerMiddle = GameFieldLayer.COUNT_CELLS / 2;
            SnakeLayer.MoveSnakeHeadTo(GameFieldLayerMiddle, GameFieldLayerMiddle);
            //GameField.UpdateTableField(middlePos, middlePos, ElementType.SnakeHead);

            //snakeHead = GameField.GetTableElement(middlePos, middlePos);

            //snakeBody = new ElementBase[GameField.TableSize * GameField.TableSize];
            //snakeBody[0] = snakeHead;
            //currentSnakeLenght++;

            //GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            //snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            //currentSnakeLenght++;

            //GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            //snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            //currentSnakeLenght++;

            //GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            //snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            //currentSnakeLenght++;

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

            //var middlePos = GameField.SIZE / 2;
            //snakeHead = GameField.UpdateFieldElement(middlePos, middlePos, SNAKE_HEAD_BRUSH);
            //UpdateHeadIndexes(middlePos, middlePos);
            //GameField.Invalidate();

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
            Timer.Enabled = false;

            // Если нельзя идти в противоположную сторону
            currentSnakeDirection =
                currentSnakeDirection == Keys.Up && clickedArrow == Keys.Down
                || currentSnakeDirection == Keys.Down && clickedArrow == Keys.Up
                || currentSnakeDirection == Keys.Left && clickedArrow == Keys.Right
                || currentSnakeDirection == Keys.Right && clickedArrow == Keys.Left
                ? currentSnakeDirection
                : clickedArrow;

            //currentSnakeDirection = clickedArrow; Если можно идти в противоположную сторону
            switch (currentSnakeDirection)
            {
                case Keys.Up:
                    SnakeLayer.MoveSnakeHeadTo(--SnakeLayer.snakeHeadI, SnakeLayer.snakeHeadJ);
                    break;
                case Keys.Down:
                    SnakeLayer.MoveSnakeHeadTo(++SnakeLayer.snakeHeadI, SnakeLayer.snakeHeadJ);
                    break;
                case Keys.Left:
                    SnakeLayer.MoveSnakeHeadTo(SnakeLayer.snakeHeadI, --SnakeLayer.snakeHeadJ);
                    break;
                case Keys.Right:
                    SnakeLayer.MoveSnakeHeadTo(SnakeLayer.snakeHeadI, ++SnakeLayer.snakeHeadJ);
                    break;
            }

            //if (currentSnakeDirection == Keys.Up && snakeHeadNewI < 0
            //    || currentSnakeDirection == Keys.Down && snakeHeadNewI >= GameField.TableSize
            //    || currentSnakeDirection == Keys.Left && snakeHeadNewJ < 0
            //    || currentSnakeDirection == Keys.Right && snakeHeadNewJ >= GameField.TableSize)
            //{
            //    MessageBox.Show($"YOU LOSE! OUT OF { clickedArrow }");
            //    return;
            //}

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
            Timer.Enabled = true;
        }

        //GameField.UpdateFieldElementToBase(headI, headJ);
        //snakeHead = GameField.UpdateFieldElement(snakeHeadNewI, snakeHeadNewJ, SNAKE_HEAD_BRUSH);

        //snakeHead = GameField.UpdateTableField(snakeHead, snakeHeadNewI, snakeHeadNewJ);
        //snakeHead.Type = ElementType.SnakeHead;
        //snakeHead.UpdateColor();
        //snakeBody[0] = snakeHead;

        //    Timer.Enabled = true;
        //}


        private bool isStarted { get; set; }
        private bool onPause { get; set; }
        private Keys clickedArrow { get; set; }
        private Keys currentSnakeDirection { get; set; }

        private readonly IRandomService randomService;
    }
}
