using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using HotelBooking;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    [TestClass]
    public class BookingRepoTest
    {
        private ServiceProvider _serviceProvider;
        private string connectionString;
        private string basePathAPI;
        private ConfigurationHelper config = new ConfigurationHelper();
        private IBookingRepo repo;

        public BookingRepoTest()
        {

            var webHost = WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>()
               .Build();
            _serviceProvider = new ServiceProvider(webHost);
            config.ResolvePaths(out connectionString, out basePathAPI);
            
        }

        [TestMethod]
        public void GetRoomDetailsTest()
        {
            //arrange
            //repo = new BookingRepo(connectionString);
            repo = _serviceProvider.GetService<IBookingRepo>();
            Room room = new Room();
            room.Id = 1;
            room.status = "A bit dusty";
            room.PricePerDay = 12.5F;
            //act
            var res = repo.GetRoomDetails(room.Id);

            //assert
            Assert.AreEqual(room.Id, res.Id);
            Assert.AreEqual(room.status, res.status);
            Assert.AreEqual(room.PricePerDay, res.PricePerDay);
        }

    }
}
