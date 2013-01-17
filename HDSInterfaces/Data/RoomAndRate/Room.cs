using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Room : BaseItinerary 
    {
        //room information
        public RoomInformation RoomInfo { get; set; }

        //rate and cancellation policy
        public CancellationPolicy CancellationPolicy { get; set; }
        public RoomRate Rates { get; set; }

    }
}
