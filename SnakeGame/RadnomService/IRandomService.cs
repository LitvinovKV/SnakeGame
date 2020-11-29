using SnakeGame.Fruits;
using System.Drawing;

namespace SnakeGame.RadnomService
{
    public interface IRandomService
    {
        Point GeneratePoint(int leftX, int leftY, int rightX, int rightY);
        FruitBase GenerateFruit(int leftX, int leftY, int rightX, int rightY);
    }
}
