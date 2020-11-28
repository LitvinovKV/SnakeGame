using SnakeGame.Fruits;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SnakeGame.RadnomService
{
    public class RandomService : IRandomService
    {
        private Random rand { get; } = new Random();

        private Type[] fruitTypes { get; } = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && type.BaseType == typeof(FruitBase))
            .ToArray();

        public Point GeneratePoint(int leftX, int leftY, int rightX, int rightY)
            => new Point() { X = rand.Next(leftX, rightX + 1), Y = rand.Next(leftY, rightY + 1) };

        public FruitBase GenerateFruit(int leftX, int leftY, int rightX, int rightY)
        {
            var randPoint = GeneratePoint(leftX, leftY, rightX, rightY);
            return (FruitBase) Activator.CreateInstance(fruitTypes[rand.Next(0, fruitTypes.Length)], new object[] { randPoint.X, randPoint.Y });
        }

        // TODO каждый раз не генерировать новый фрукт, а просто менять координаты сущетсвующего
        public FruitBase GenerateFruitNotIn(int leftX, int leftY, int rightX, int rightY, IEnumerable<ElementBase> elements)
        {
            while (true)
            {
                var randFruit = GenerateFruit(leftX, leftY, rightX, rightY);
                
                foreach(var element in elements)
                {
                    if (!randFruit.Contains(element))
                    {
                        return randFruit;
                    }
                }
            }
        }
    }
}
