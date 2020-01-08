using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using HotelBooking;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services.Interfaces;

namespace Testing.End_To_End
{
    [TestClass]
    public class UnitTestsBooking
    {
        private ServiceProvider _serviceProvider;
        private ConfigurationHelper config = new ConfigurationHelper();
        private string connectionString;
        private IBookingRepo repo;
        private IBookingService service;
        private string basePathAPI;
        
        public UnitTestsBooking ()
        {
            var webHost = WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>()
               .Build();
            _serviceProvider = new ServiceProvider(webHost);
            config.ResolvePaths(out connectionString, out basePathAPI);
        }

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
