using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Room : BaseItinerary 
    {
        //room info
        public List<Amenities> Amenities { get; set; }

        //images
        public List<RoomImage> Images { get; set; }

        //rate and cancellation policy
        public CancellationPolicy CancellationPolicy { get; set; }
        public RoomRate Rates { get; set; }

        


    }
}
