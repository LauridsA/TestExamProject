using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using HotelBooking;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services.Interfaces;
using System;

namespace Testing.UT
{
    [TestClass]
    public class StructureBased
    {
        private ServiceProvider _serviceProvider;
        private ConfigurationHelper config = new ConfigurationHelper();
        private string connectionString;
        private IBookingService service;
        private string basePathAPI;
        
        public StructureBased ()
        {
            var webHost = WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>()
               .Build();
            _serviceProvider = new ServiceProvider(webHost);
            config.ResolvePaths(out connectionString, out basePathAPI);
        }

        [TestMethod]
        public void BookRoomService_endBeforeStart_Invalid()
        {
            //arrange
            service = _serviceProvider.GetService<IBookingService>();
            var roomId = 1;
            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(-1);
            string customerComment = "I am a fool for trying this";

            //act
            try
            {
            service.BookRoom(roomId, start, end, customerComment);
                Assert.Fail("Expected exception");
            } catch (Exception e)
            {
                Assert.AreEqual("Illegal: start should be before end", e.Message);
            }

        }
    }
}
