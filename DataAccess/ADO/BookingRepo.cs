using DataAccess.ADO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ADO
{
    public class BookingRepo : IBookingRepo
    {
        private readonly string connectionstring;
        public BookingRepo(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }
    } 
}
