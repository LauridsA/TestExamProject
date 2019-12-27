using DataAccess.ADO;
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
        private string connectionString;
        private string basePathAPI;
        private ConfigurationHelper config = new ConfigurationHelper();
        private BookingRepo repo;

        public BookingRepoTest()
        {
            //empty
            config.ResolvePaths(out connectionString, out basePathAPI);
            repo = new BookingRepo(connectionString);
        }

        [TestMethod]
        public void GetRoomDetailsTest()
        {
            //arrange
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
