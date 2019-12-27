using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    public class Room
    {
        [DataMember(Name = "RoomId", EmitDefaultValue = false)]
        public int Id { get; set; }
        [DataMember(Name = "PricePerDay", EmitDefaultValue = false)]
        public float PricePerDay { get; set; }
        public enum RoomType 
        { 
            Basic = 1,
            Standard,
            Deluxe,
            Penthouse
        }

        [DataMember(Name ="Status", EmitDefaultValue =false)]
        public string status { get; set; }
        [DataMember(Name = "Available", EmitDefaultValue = false)]
        public bool available { get; set; }

        public Room ()
        {
            //intentionally empty
        }

    }
}
