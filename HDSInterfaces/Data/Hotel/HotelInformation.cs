using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HotelInformation
    {
        //general things
        public float? StarRating { get; set; }
        public float? TripAdvisorRating { get; set; }
        public Identification Chain { get; set; }
        public string HotelDescription { get; set; }
        public int? NumberOfRoom { get; set; }
        public int? NumberOfFloor { get; set; }
        public string CurrencyCode { get; set; }
        
        //information
        public string GenerationInfo { get; set; }
        public string PropertyInfo { get; set; }
        public string RoomInfo { get; set; }
        public string AdditionalInfo { get; set; }
        public string RestaurantInfo { get; set; }

        //location
        public Address Address { get; set; }
        public string AreaInfo { get; set; }
        public string DrivingDirection { get; set; }        

        //hotel rule and policy
        public HotelPolicy PolicyInfo { get; set; }

        //amenties/facilities
        public List<Amenities> Amenities { get; set; }
        public List<Amenities> Facilities { get; set; }
        public List<Amenities> RoomAmenities { get; set; }

        //images
        public List<HotelImage> Images { get; set; }
    }
}
