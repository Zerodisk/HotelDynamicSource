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

        public override SearchResultRS GetSearchResult(HDSRequest request)
        {
            shoppingHelper = new ShoppingHelper();

            return shoppingHelper.GetSearchResult(request);
        }

        public override HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            shoppingHelper = new ShoppingHelper();

            return shoppingHelper.GetHotelAvailability(request);
        }

        public override ReservationRS MakeHotelReservation(HDSRequest request)
        {
            bookingHelper = new BookingHelper();

            return bookingHelper.MakeHotelReservation(request);
        }



    }
}
