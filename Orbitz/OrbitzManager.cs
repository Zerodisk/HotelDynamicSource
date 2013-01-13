using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDSInterfaces;

namespace Orbitz
{
    public class OrbitzManager : HDSProviderBase 
    {
        public override SearchResultByLocationRS GetSearchResultByLocation(HDSRequest request)
        {
            return null;
        }

        public override HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            return null;
        }

        public override HotelRateRuleRS GetHotelRateRule(HDSRequest request)
        {
            return null;
        }

        public override HotelReservationRS MakeHotelReservation(HDSRequest request)
        {
            return null;
        }
    }
}
