using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        bool BookRoom(int roomId, DateTime start, DateTime end);
        bool DeleteBooking(int bookingId);
        List<Booking> GetAllBookings();
        Booking GetBookingDetails(int bookingId);
    }
}
