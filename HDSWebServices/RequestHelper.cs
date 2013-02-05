using System;
using System.Collections.Generic;
using System.Web;

using HDSInterfaces;

namespace HDSWebServices
{
    public class RequestHelper
    {

        /*
         * return list of itinerary
         *  this function convert from REST request into List<Itinerary>
         *  
         * - numRoom = number of room
         * - numAdult1, numAdult2, ..... numAdultN  = number of adult for muliple room
         * - numChild1, numChild2, ..... numChildN  = string pattern n_x_y_z, n number of children, x,y and z is age of each child (e.g. 1 child age 5 = 1_5), (e.g. 2 children with age 2 and 6 = 2_2_6)
         * - roomCode1, roomCode2, ..... roomCodeN  = room unique identification code (not in use at the moment as we do hybridge mode)
         */
        public List<Itinerary> GenerateItineraryList(HttpRequest httpRq)
        {
            List<Itinerary> result = new List<Itinerary>();
            int numRoom = int.Parse(httpRq["numRoom"]);
            for (int i = 1; i < numRoom; i++)
            {
                Itinerary itinerary = new Itinerary(int.Parse(httpRq["numAdult" + i.ToString()]), httpRq["numChild" + i.ToString()]);
                result.Add(itinerary);
            }

            return result;
        }

    }
}
