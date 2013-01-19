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

        //availability rate and cancellation policy
        public int? NumberOfRoomAvailable { get; set; }
        public RoomRate Rates { get; set; }
        public CancellationPolicy CancellationPolicy { get; set; }

        //all promotion/value add/any of room complimentary
        public List<Promotion>    Promotions { get; set; }
        public List<RoomValueAdd> ValueAdds { get; set; }

    }
}
