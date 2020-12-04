using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame.RadnomService
{
    public interface IRandomService
    {
         ElementType GenerateFruit(int tableSize, IList<ElementBase> snakeBody);
    }
}
