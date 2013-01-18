using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class SearchResultRS : BaseResponse 
    {

        public bool? IsMoreResultsAvailable { get; set; }          //true if there is further result, which require next request (pagination)
        public int TotalHotelReturnThisResult { get; set; }        //number of hotel return in this query
        public int TotalHotelReturnThisSearch { get; set; }        //total number of hotels return from the search criteria

        public List<Hotel> Hotels { get; set; }
    }
}
