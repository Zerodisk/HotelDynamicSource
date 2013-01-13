using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDSInterfaces;

namespace Expedia
{
    public class ExpediaManager : HDSProviderBase 
    {
        ShoppingHelper shoppingHelper;
        BookingHelper bookingHelper;

        public ExpediaManager(){
 
        }

        public override SearchResultByLocationRS GetSearchResultByLocation(HDSRequest request)
        {
            shoppingHelper = new ShoppingHelper();

            return shoppingHelper.GetSearchResultByLocation(request);
        }

        public override HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            shoppingHelper = new ShoppingHelper();

            return shoppingHelper.GetHotelAvailability(request);
        }

        public override HotelReservationRS MakeHotelReservation(HDSRequest request)
        {
            bookingHelper = new BookingHelper();


            return bookingHelper.MakeHotelReservation(request);
        }



    }
}
