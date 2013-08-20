using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class RoomInfo : Identification 
    {
        //description
        public string Description { get; set; }        

        //bedding
        public string BeddingDescription { get; set; }

        //room info
        public List<Amenity> Amenities { get; set; }

        //images
        public List<RoomImage> Images { get; set; }
    }
}
