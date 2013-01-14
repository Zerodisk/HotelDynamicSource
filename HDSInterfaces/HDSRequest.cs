using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HDSRequest: BaseRequest 
    {
        //date stay
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        //location      - request for search by location

        //hotel         - request for search by a hotel

        //itinerary     - request for search by room (rate rule)

        //reservation   - request for booking
        
    }
}
