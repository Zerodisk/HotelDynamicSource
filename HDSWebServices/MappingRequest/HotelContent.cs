using System;
using System.Collections.Generic;
using System.Web;

using HDSInterfaces;

namespace HDSWebServices
{
    public class HotelContent
    {
        public HDSRequest MapHotelContent(HDSRequest request, HttpRequest httpRq)
        {
            long hotelId = long.Parse(httpRq["hotelID"]);
            request.Hotels = new List<Hotel>();
            request.Hotels.Add(new Hotel { Id = hotelId });
            return request;
        }
    }
}
