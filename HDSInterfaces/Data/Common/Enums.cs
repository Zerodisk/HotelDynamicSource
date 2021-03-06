﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public enum HDSSource
    {
        Local    = 0,
        Expedia  = 1,
        Orbitz   = 2
    }

    public enum HDSRequestType
    {
        NoType = 0,

        //search result
        SearchByLocationKeyword = 10,
        SearchByLocationIds     = 11,
        SearchByHotelIds        = 12,

        //search by a single hotel or with room specific
        SearchByHotelId         = 20,
        SearchByHotelIdWithRoom = 21,

        //booking
        Reprice                 = 30,
        Reservation             = 31,

        //post-booking, itinerary and cancellation   
        Itinerary               = 40,
        PreCancellation         = 41,
        Cancellation            = 42,

        //contents
        HotelContent            = 50
    }

    public enum HDSGuestType
    {
        Adult  = 0,
        Child  = 1,
        Senior = 2
    }

    public enum HDSItineraryType
    {
        Room     = 0,
        ExtraBed = 1,
    }

    public enum HDSSmokingPreference
    {
        NoPreference = 0,
        Smoking      = 1,
        NonSmoking   = 2
    }

    public enum HDSCreditCardType
    {
        NoCard = 0,
        VISA   = 1,
        MC     = 2,
        AMEX   = 3
    }
}
