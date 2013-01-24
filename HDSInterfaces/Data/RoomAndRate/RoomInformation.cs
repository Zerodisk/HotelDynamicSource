using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class RoomInformation
    {
        //bedding
        public string BeddingDescription { get; set; }

        //room info
        public List<Amenities> Amenities { get; set; }

        //images
        public List<RoomImage> Images { get; set; }
    }
}
