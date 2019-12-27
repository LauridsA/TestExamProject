using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    [TestClass]
    public class BookingControllerTest
    {
        private string connectionString;
        private string basePathAPI;
        private ConfigurationHelper config = new ConfigurationHelper();
        public BookingControllerTest ()
        {
            //empty
        }

        [TestMethod]
        public void GetDetailsOfRoomTest()
        {
            //BasePath to API..
            
            
            //act

            
            //assert


        }

        [TestMethod]
        public void NegativeTestFail()
        {
            config.ResolvePaths(out connectionString, out basePathAPI);
            Assert.Fail("ConnectionString: "+ connectionString);
        }
    }
}
