using System.Collections.Generic;

namespace SnakeGame.RadnomService
{
    public interface IRandomService
    {
        (int, int)? GenerateIndexesExpect(IEnumerable<(int, int)> set1, IEnumerable<(int, int)> set2);
    }
}
