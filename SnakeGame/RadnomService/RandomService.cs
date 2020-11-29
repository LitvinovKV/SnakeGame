using SnakeGame.Fruits;
using System;
using System.Drawing;
using System.Linq;

namespace SnakeGame.RadnomService
{
    public class RandomService : IRandomService
    {
        private Random rand { get; } = new Random();

        private FruitBase[] fruits { get; } = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && type.BaseType == typeof(FruitBase))
            .Select(type => (FruitBase)Activator.CreateInstance(type, new object[] { 0, 0 }))
            .ToArray();

        public Point GeneratePoint(int leftX, int leftY, int rightX, int rightY)
            => new Point() { X = rand.Next(leftX, rightX + 1), Y = rand.Next(leftY, rightY + 1) };

        public FruitBase GenerateFruit(int leftX, int leftY, int rightX, int rightY)
        {
            var randPoint = GeneratePoint(leftX, leftY, rightX, rightY);
            var fruit = fruits[rand.Next(0, fruits.Length)];
            fruit.Location = randPoint;
            return fruit;
        }
    }
}
