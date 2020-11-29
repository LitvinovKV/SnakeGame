using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame.Fruits;

namespace UnitTests
{
    [TestClass]
    public class ElementTests
    {
        [TestMethod]
        public void ElementIncludeTests()
        {
            var element = new Apple(20, 20);

            var element2 = new Apple(1, 1);
            Assert.AreEqual(true, element.Include(element2));

            element2 = new Apple(39, 1);
            Assert.AreEqual(true, element.Include(element2));

            element2 = new Apple(1, 39);
            Assert.AreEqual(true, element.Include(element2));

            element2 = new Apple(39, 39);
            Assert.AreEqual(true, element.Include(element2));
        }

        [TestMethod]
        public void ElementNotIncludeTests()
        {
            var element = new Apple(20, 20);

            var element2 = new Apple(-10, 20);
            Assert.AreEqual(false, element.Include(element2));

            element2 = new Apple(41, 20);
            Assert.AreEqual(false, element.Include(element2));

            element2 = new Apple(20, 41);
            Assert.AreEqual(false, element.Include(element2));

            element2 = new Apple(41, 41);
            Assert.AreEqual(false, element.Include(element2));
        }
    }
}
