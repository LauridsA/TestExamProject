using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            //intentionally empty
        }

        [TestMethod]
        public async void GetDetailsOfRoomTest()
        {
            //arrange
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(basePathAPI);

            //act
            var res = await client.GetAsync("api/Booking/2");
            
            //assert
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

        }

        [TestMethod]
        public void NegativeTestFail()
        {
            config.ResolvePaths(out connectionString, out basePathAPI);
            Assert.Fail("ConnectionString: "+ connectionString);
        }
    }
}
