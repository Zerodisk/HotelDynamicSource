using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Hotel : Identification
    {
        //hotel information
        public HotelInformation HotelInfo { get; set; }

        //rooms information and rates
        public List<Room> Rooms { get; set; } 
    }
}
