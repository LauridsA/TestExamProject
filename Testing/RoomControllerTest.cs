using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Testing
{
    [TestClass]
    public class RoomControllerTest
    {
        private string connectionString;
        private string basePathAPI;
        private ConfigurationHelper config = new ConfigurationHelper();

        public RoomControllerTest()
        {
            config.ResolvePaths(out connectionString, out basePathAPI);
        }

        [TestMethod]
        public void GetDetailsOfRoomTest()
        {
            //arrange
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(basePathAPI);

            //act
            var res = client.GetAsync("api/Room/checkAvailablity/2").Result;
            
            //assert
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

        }

        [TestMethod]
        public void NegativeTestFail()
        {
            
            Assert.Fail("ConnectionString: "+ connectionString + " and base path was found to be " + basePathAPI);
        }
    }
}
