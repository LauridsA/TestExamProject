using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BookingRequest
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string customerComment { get; set; }
    }
}
