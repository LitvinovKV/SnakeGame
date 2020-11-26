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
        public static int FIELD_HEIGHT = 20;

        public Timer Timer { get; private set; }

        public Game(PictureBox field)
        {
            isStarted = false;
            onPause = true;
            this.field = field;
            snake = new Snake((this.field.Location.X + this.field.Width) / 2, (this.field.Location.Y + this.field.Height) / 2);

            // Под вопросом. Как будут отображать и добовлять элементы на игровое поле
            AddElementOnField(snake.body[0]);

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

            snake.Direction = key;
        }

        public void SpaceButtonClick()
        {
            Debug.WriteLine("SPACE KEY CLICKED!");

            onPause = !onPause & isStarted;
            Timer.Enabled = onPause;
            
            Debug.WriteLine("GAME " + (onPause ? "PAUSED" : "RESUMED"));
        }

        private void AddElementOnField(Control element)
            => field.Controls.Add(element);
        
        private void StartGame()
        {
            Debug.WriteLine("GAME STARTED");
            isStarted = true;
            Timer.Enabled = true;
        }

        private void MoveSnake(object sender, EventArgs e)
        {
            var oldPoint = snake.body[0].Location;
            snake.body[0].Location = new Point(oldPoint.X += 10, oldPoint.Y);
        }

        private PictureBox field { get; set; }
        private Snake snake { get; set; }
        private bool isStarted { get; set; }
        private bool onPause { get; set; }
    }
}
