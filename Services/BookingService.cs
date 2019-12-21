using DataAccess.ADO;
using DataAccess.ADO.Interfaces;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BookingService : IBookingService
    {
        private BookingRepo repo;
        public BookingService(IServiceProvider services)
        {
            this.repo = (BookingRepo)services.GetService(typeof(IBookingRepo));
        }
        public Room GetRoom(int i)
        {
            return repo.GetRoomDetails(1);
        }

        public bool BookRoom()
        {
            return repo.BookRoom(1);
        }

        public List<Room> GetAllAvailableRooms(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }


        public bool IsBooked(int roomId)
        {
            throw new NotImplementedException();
        }

        public bool UnBookRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
