using DataAccess.ADO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BookingService : IBookingService
    {
        public BookingService()
        {
            BookingRepo da = new BookingRepo();
        }
    }
}
