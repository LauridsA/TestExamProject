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
        private IBookingRepo repo;
        private IRoomRepo roomRepo;
        public BookingService(IBookingRepo repo, IRoomRepo roomRepo)
        {
            this.repo = repo;
            this.roomRepo = roomRepo;
        }

        public bool BookRoom(int roomId, DateTime start, DateTime end)
        {
            Room room = roomRepo.GetRoomDetails(roomId);
            if (!room.available)
                throw new Exception("Room is not available!");
            return repo.BookRoom(room, start, end);
        }

        public bool DeleteBooking(int bookingId)
        {
            return repo.DeleteBooking(bookingId);
        }

        public List<Booking> GetAllBookings()
        {
            return repo.GetAllBookings();
        }

        public Booking GetBookingDetails(int bookingId)
        {
            
            return repo.GetBookingDetails(bookingId);
        }
    }
}
