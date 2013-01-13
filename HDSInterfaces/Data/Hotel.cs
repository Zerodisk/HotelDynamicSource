using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Hotel : BaseHotel 
    {
        public Identification Chain { get; set; }

        public decimal StarRating { get; set; }

        //hotel location
        public Address Address { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        //hotel information
        public HotelInformation HotelInfo { get; set; }

        //hotel rule and policy
        public HotelPolicy PolicyInfo { get; set; }

        //amenties/facilities
        public List<Amenities> Amenities { get; set; }

        //images
        public List<HotelImage> Images { get; set; }

        //rooms information and rates
        public List<Itinerary> Rooms { get; set; } 
    }
}
