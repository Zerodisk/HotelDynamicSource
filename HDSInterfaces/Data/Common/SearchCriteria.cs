using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class SearchCriteria
    {
        public Location Location { get; set; }
        public string LocationKeyword { get; set; }         //for search by location keyword
        public string HotelNameKeyword { get; set; }        //additional filter by hotel name keyword 
        public decimal? MaxDistance { get; set; }
        public Address Address { get; set; }

        public float? MinStarRating { get; set; }
        public float? MaxStarRating { get; set; }

        public List<Amenity> Amenities { get; set; }            //additional filter by list of amenities
        public List<PropertyType> PropertyTypes { get; set; }   //additional filter by list of property type
     
    }
}
