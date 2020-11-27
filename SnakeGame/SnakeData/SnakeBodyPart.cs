﻿using System.Drawing;

namespace SnakeGame.SnakeData
{
    public class SnakeBodyPart : ElementBase
    {
        public SnakeBodyPart(int x, int y) : base(x, y)
        {
            BackColor = Color.Red;
        }

        protected override string ImagePath { get; set; }
    }
}
