using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ADO.Interfaces
{
    public interface IBookingRepo
    {
        bool BookRoom(Room room, DateTime start, DateTime end);
        bool DeleteBooking(int bookingId);
        List<Booking> GetAllBookings();
        Booking GetBookingDetails(int bookingId);
    }
}
