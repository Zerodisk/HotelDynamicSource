using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class SearchCriteria
    {
        public List<Location> Locations { get; set; }
        public string  LocationKeyword { get; set; }
        public decimal? MaxDistance { get; set; }
        public Address Address { get; set; }

        public float? MinStarRating { get; set; }
        public float? MaxStarRating { get; set; }

     
    }
}
