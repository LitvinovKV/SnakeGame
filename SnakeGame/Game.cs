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

        public Game(PictureBox field)
        {
            Field = field;

            InitTimer();
        }

        private void InitTimer()
        {
            Timer = new Timer();
            Timer.Interval = 500;
        }

        private PictureBox Field { get; set; }
        private Snake snake { get; set; }
    }
}
