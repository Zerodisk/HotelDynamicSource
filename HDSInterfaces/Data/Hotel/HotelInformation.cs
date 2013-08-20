using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HotelInformation : Identification
    {
        //general things
        public float? StarRating { get; set; }
        public float? TripAdvisorRating { get; set; }
        public Identification Chain { get; set; }        
        public string CurrencyCode { get; set; }
        
        //information
        public string HotelDescription { get; set; }
        public string PropertyInfo { get; set; }
        public string RoomInfo { get; set; }
        public string AdditionalInfo { get; set; }
        public string RestaurantInfo { get; set; }

        //location
        public string AreaInfo { get; set; }
        public string DrivingDirection { get; set; }
        public Address Address { get; set; }

        //hotel rule and policy
        public HotelPolicy PolicyInfo { get; set; }

        //amenties-facilities
        public List<Amenity> Amenities { get; set; }
        public List<Facility> Facilities { get; set; }
        //public List<Amenity> RoomAmenities { get; set; }

        //images
        public List<HotelImage> Images { get; set; }

        //room info
        public List<RoomInfo> RoomInfos { get; set; }

        //guet review and score
        public GuestReview GuestReview { get; set; }


        //for xml serialization things
        //[System.Xml.Serialization.XmlIgnore]
        //public bool StarRatingSpecified { get { return this.StarRating != null; } }
        //[System.Xml.Serialization.XmlIgnore]
        //public bool TripAdvisorRatingSpecified { get { return this.TripAdvisorRating != null; } }
    }
}
