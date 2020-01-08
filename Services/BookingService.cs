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

        public bool BookRoom(int roomId, DateTime newBookingStart, DateTime newBookingEnd, string customerComment)
        {
            //is the data valid?
            if (newBookingStart.Date > newBookingEnd.Date)
                throw new ArgumentException("Illegal: start should be before end");

            //which room are we dealing with?
            Room room = roomRepo.GetRoomDetails(roomId);

            //Is the room available at this time?
            var bookings = repo.GetBookingsByRoom(room);
            foreach (var item in bookings)
            {
                if (!((item.startDate.Date > newBookingStart.Date && item.startDate.Date > newBookingEnd.Date ) || (newBookingStart.Date > item.endDate.Date && newBookingEnd.Date >item.endDate.Date)))
                    throw new Exception("Room is not available in this period! It clashed with Booking ID: "+item.Id);
            }
                
            //all is well. Let's create the booking.
            Booking booking = new Booking();
            booking.room = room;
            booking.startDate = newBookingStart;
            booking.endDate = newBookingEnd;
            booking.totalPrice = (newBookingEnd.Day + 1 - newBookingStart.Day) * room.PricePerDay;
            booking.dateOfOrder = DateTime.Today;
            booking.customerComment = customerComment;
            return repo.BookRoom(booking);
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
