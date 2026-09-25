using TestsOfTests;
namespace ActualTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            int x = 10;
            int y = 10;

            // Act
            int res = MyMath.Somme(x, y);


            //Assert
            Assert.AreEqual(20, res);
        }
    }
}
