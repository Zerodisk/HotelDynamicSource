using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HDSRequest: BaseRequest 
    {
        //date stay range
        public StayDate StayDate { get; set; }

        //location      - request for search by location
        public SearchCriteria SearchCriteria { get; set; }

        //hotel         - request for search by a hotel and list of hotels
        public List<Hotel> Hotels { get; set; }

        //itinerary     - request for search by room (rate rule)
        public List<Itinerary> Itineraries { get; set; }

        //reservation   - request for booking/customer details/credit card details
        public ReservationInformation ReservationInfo { get; set; }

        //error in request
        public WarningAndError Error { get; set; }

        /*
         * this is the onle single construction and it needed type of request in order to create this request object
         */ 
        public HDSRequest(HDSRequestType requestType)
        {
            Session = new Session();
            Session.PageIndex       = 1;
            Session.PageSize        = 50;
            Session.Locale          = "en_US";
            Session.SourceProvider  = HDSSource.Expedia;

            RequestType = requestType;
            IsContentRequested = false;      
        }

        public bool IsRequestError
        {
            get
            {
                if (Error == null)
                    return false;   //no error
                else
                    return true;
            }
        }
        
    }
}
