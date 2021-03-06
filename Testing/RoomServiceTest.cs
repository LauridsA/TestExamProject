﻿using HotelBooking;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing
{
    [TestClass]
    public class RoomServiceTest
    {
        private IRoomService _service;
        private ServiceProvider _serviceProvider;

        public RoomServiceTest()
        {
            var webHost = WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>()
               .Build();
            _serviceProvider = new ServiceProvider(webHost);
            //empty?
        }

        [TestMethod]
        public void Test()
        {
            //arrange
            _service = _serviceProvider.GetService<IRoomService>();

            //act
            var res = _service.GetRoom(1);

            //assert
            Assert.IsNotNull(res);
        }
    }
}
