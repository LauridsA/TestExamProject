using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ADO.Interfaces
{
    public interface IBookingRepo
    {
        bool BookRoom(Booking booking);
        bool DeleteBooking(int bookingId);
        List<Booking> GetAllBookings();
        Booking GetBookingDetails(int bookingId);
        List<Booking> GetBookingsByRoom(Room room);
    }
}
