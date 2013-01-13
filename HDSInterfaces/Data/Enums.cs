using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public enum HDSSource
    {
        NoSource = 0,
        Expedia = 1,
        Orbitz = 2
    }

    public enum HDSRequestType
    {
        NoType = 0,

        //search result
        SearchByLocationKeyword = 10,
        SearchByLocationIds = 11,
        SearchByHotelIds = 12,

        //hotel page
        SearchByHotelId = 20,

        //booking
        Reprice = 30,
        Reservation = 31
    }
}
