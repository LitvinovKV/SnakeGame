using SnakeGame.RadnomService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            currentSnakeDirection = Keys.None;
            
            GameField = new GameField(fieldX, fieldY);
            var middlePos = GameField.TableSize / 2;
            GameField.UpdateTableField(middlePos, middlePos, ElementType.SnakeHead);
            
            snakeHead = GameField.GetTableElement(middlePos, middlePos);

            snakeBody = new ElementBase[GameField.TableSize * GameField.TableSize];
            snakeBody[0] = snakeHead;
            currentSnakeLenght++;

            GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            currentSnakeLenght++;

            GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            currentSnakeLenght++;

            GameField.UpdateTableField(middlePos + currentSnakeLenght, middlePos, ElementType.SnakeBodyPart);
            snakeBody[currentSnakeLenght] = GameField.GetTableElement(middlePos + currentSnakeLenght, middlePos);
            currentSnakeLenght++;

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

            var snakeHeadNewI = snakeHead.I;
            var snakeHeadNewJ = snakeHead.J;
            switch (currentSnakeDirection)
            {
                case Keys.Up:
                    snakeHeadNewI--;
                    break;
                case Keys.Down:
                    snakeHeadNewI++;
                    break;
                case Keys.Left:
                    snakeHeadNewJ--;
                    break;
                case Keys.Right:
                    snakeHeadNewJ++;
                    break;
            }

            if (currentSnakeDirection == Keys.Up && snakeHeadNewI < 0
                || currentSnakeDirection == Keys.Down && snakeHeadNewI >= GameField.TableSize
                || currentSnakeDirection == Keys.Left && snakeHeadNewJ < 0
                || currentSnakeDirection == Keys.Right && snakeHeadNewJ >= GameField.TableSize)
            {
                MessageBox.Show($"YOU LOSE! OUT OF { clickedArrow }");
                return;
            }

            // TODO Попытаться придумать что-то с обновлением цвета ячейки по типу автоматически
            if (currentSnakeLenght > 1)
            {
                snakeBody[currentSnakeLenght - 1].Type = ElementType.BaseField;
                snakeBody[currentSnakeLenght - 1].UpdateColor();

                for (var i = currentSnakeLenght - 1; i >= 1; i--)
                {
                    snakeBody[i] = snakeBody[i - 1];
                    snakeBody[i].Type = ElementType.SnakeBodyPart;
                    snakeBody[i].UpdateColor();
                }
            }

            snakeHead = GameField.UpdateTableField(snakeHead, snakeHeadNewI, snakeHeadNewJ);
            snakeHead.Type = ElementType.SnakeHead;
            snakeHead.UpdateColor();
            snakeBody[0] = snakeHead;

            Timer.Enabled = true;
        }

        private bool isStarted { get; set; }
        private bool onPause { get; set; }
        private Keys clickedArrow { get; set; }
        private Keys currentSnakeDirection { get; set; }
        private ElementBase snakeHead { get; set; }
        private ElementBase[] snakeBody { get; set; }
        private int currentSnakeLenght { get; set; }

        private readonly IRandomService randomService;
    }
}
