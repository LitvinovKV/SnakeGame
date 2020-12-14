using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame.RadnomService;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class RandomServiceTests
    {
        private readonly IRandomService randomService;

        private (int, int)[] set1;
        private (int, int)[] set2;

        public RandomServiceTests()
        {
            randomService = new RandomService();
        }

        [TestMethod]
        public void GenerateIndexesExpectTestCases()
        {
            set1 = new[] { (1, 1), (2, 2), (3, 3), (4, 4) };
            set2 = new[] { (1, 1), (2, 2), (4, 4) };
            var expected1 = (3, 3);

            var actual = randomService.GenerateIndexesExpect(set1, set2);
            
            Assert.AreEqual(expected1, actual);

            set2 = new[] { (1, 1), (2, 2) };
            var expected2 = new[] { (3, 3), (4, 4) };
            actual = randomService.GenerateIndexesExpect(set1, set2);

            Assert.AreEqual(true, expected2.Contains(actual.Value));

            set2 = new[] { (1, 1), (2, 2), (3, 3), (4, 4) };
            actual = randomService.GenerateIndexesExpect(set1, set2);
            Assert.AreEqual(null, actual);
        }
    }
}
