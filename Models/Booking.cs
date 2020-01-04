using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Booking
    {
        public Booking ()
        {
            //intentionally empty
        }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int roomId { get; set; }
        public float totalPrice { get; set; }
        public string customerComment { get; set; }
        public DateTime dateOfOrder { get; set; }
    }
}
