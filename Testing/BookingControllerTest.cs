using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    [TestClass]
    public class BookingControllerTest
    {
        private string baseUrl;
        public BookingControllerTest ()
        {
            var config = new ConfigurationHelper();
            config.ResolvePaths(out baseUrl);
        }

        [TestMethod]
        public void GetDetailsOfRoomTest()
        {
            //Connectionstrings to DB -- override
            
            
            //act

            
            //assert


        }

        [TestMethod]
        public void NegativeTestFail()
        {
            Assert.Fail("Failed because I want it to");
        }
    }
}
