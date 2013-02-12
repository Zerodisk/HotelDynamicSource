using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class SearchResultRS : BaseResponse 
    {
        public bool? IsMoreResultsAvailable { get; set; }       //true if there is further result, which require next request (pagination)
        public string NextPageUrl { get; set; }                 //only got value if IsMoreResultsAvailable = true
                                                                //   this is the next page URL if there is any
        public int TotalHotelReturnThisResult{                  //number of hotel return in this query/request
            get {
                if (Hotels == null)
                    return 0;
                else
                    return Hotels.Count;
            } 
        }  

        //return list of expected/guest location in case of location keyword request can't be found
        public List<Location> Locations { get; set; }

        //return list of hotel, null will be returned if not found any hotels
        public List<Hotel> Hotels { get; set; }

    }
}
