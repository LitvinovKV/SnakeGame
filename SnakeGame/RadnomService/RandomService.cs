using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.RadnomService
{
    public class RandomService : IRandomService
    {
        private Random rand { get; } = new Random();

        public (int, int)? GenerateIndexesExpect(IEnumerable<(int, int)> set1, IEnumerable<(int, int)> set2)
        {
            var exceptedSet = set1.Except(set2);
            var expectedSetCount = exceptedSet.Count();

            if (expectedSetCount == 0)
            {
                return null;
            }

            return exceptedSet.ElementAt(rand.Next(0, expectedSetCount));
        }
    }
}
