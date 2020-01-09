using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using HotelBooking;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Models;
using Services.Interfaces;
using System;

namespace Testing.Integration
{
    [TestClass]
    public class StructureBased
    {
        private ServiceProvider _serviceProvider;
        private ConfigurationHelper config = new ConfigurationHelper();
        private string connectionString;
        private IBookingRepo repo;
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
        public void BookingService_FarIntoTheFuture_Valid()
        {
            //arrange
            repo = _serviceProvider.GetService<IBookingRepo>();
            Booking booking = new Booking()
            {
                customerComment = "Just For the night, Thank you!",
                dateOfOrder = DateTime.Today.AddDays(200),
                startDate = DateTime.Today.AddDays(200),
                endDate = DateTime.Today.AddDays(200),
                Id = 9999999,
                totalPrice = 55.44f,
                room = new Room()
                {
                    Id = 999,
                    available = true,
                    PricePerDay = 55.44f,
                    roomType = Room.RoomType.Standard,
                    status = "it's quite pretty"
                }
            };

            //act
            var res = repo.BookRoom(booking);

            //assert
            Assert.AreEqual(true, res);
        }
    }
}
