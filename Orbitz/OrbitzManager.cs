using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDSInterfaces;

namespace Orbitz
{
    public class OrbitzManager : HDSProviderBase 
    {
        public override SearchResultRS GetSearchResultByLocation(HDSRequest request)
        {
            return null;
        }

        public override HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            return null;
        }

        public override RateRuleRS GetHotelRateRule(HDSRequest request)
        {
            return null;
        }

        public override ReservationRS MakeHotelReservation(HDSRequest request)
        {
            return null;
        }
    }
}
