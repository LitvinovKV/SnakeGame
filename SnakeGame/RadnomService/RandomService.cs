﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeGame.RadnomService
{
    public class RandomService : IRandomService
    {
        private Random rand { get; } = new Random();

        public ElementType GenerateFruit(int tableSize, IList<ElementBase> snakeBody)
        {
            throw new NotImplementedException();
        }
    }
}
