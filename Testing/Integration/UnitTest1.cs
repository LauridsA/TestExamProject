using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Integration
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            var senselessVariable = 1;

            //act
            var res = senselessVariable + 1;

            //assert
            Assert.AreEqual(res, senselessVariable + 1);
        }
    }
}
