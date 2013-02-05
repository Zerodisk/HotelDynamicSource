using System;
using System.Collections.Generic;
using System.Web;

using HDSInterfaces;

namespace HDSWebServices
{
    public class HotelAvailability
    {
        RequestHelper helper = new RequestHelper();
       
        public HDSRequest MapSearchByHotelId(HDSRequest request, HttpRequest httpRq)
        {
            //checkin-out
            request.StayDate = new StayDate { CheckIn = httpRq["checkIn"], CheckOut = httpRq["checkOut"] };

            //hotel id
            request.Hotels = new List<Hotel>();
            request.Hotels.Add(new Hotel { Id = long.Parse(httpRq["hotelId"]) });

            //room(s) request
            request.Itineraries = helper.GenerateItineraryList(httpRq);

            //if content is also requested
            if (!string.IsNullOrEmpty(httpRq["contentRequested"]))
            {
                request.IsContentRequested = bool.Parse(httpRq["contentRequested"]);
            }

            return request;
        }
    }
}
