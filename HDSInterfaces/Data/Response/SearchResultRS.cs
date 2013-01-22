using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class SearchResultRS : BaseResponse 
    {

        public bool? IsMoreResultsAvailable { get; set; }                       //true if there is further result, which require next request (pagination)
        public int TotalHotelReturnThisResult { get { return Hotels.Count; } }  //number of hotel return in this query
        public int TotalHotelReturnThisSearch { get; set; }                     //total number of hotels return from the search criteria

        //return list of expected/guest location in case of location keyword request can't be found
        public List<Location> Locations { get; set; }

        public List<Hotel> Hotels { get; set; }

        public SearchResultRS()
        {
            Hotels = new List<Hotel>();
        }
    }
}
