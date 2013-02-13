using System;
using System.Collections.Generic;
using System.Text;
using HDSInterfaces;

namespace Expedia
{
    public class ExpediaManager : HDSProviderBase 
    {
        ContentManager  contentHelper;
        ShoppingManager shoppingHelper;
        BookingManager  bookingHelper;

        public ExpediaManager(){
 
        }

        public override HotelContentRS GetHotelInfo(HDSRequest request)
        {
            contentHelper = new ContentManager();

            return contentHelper.GetHotelInfo(request);
        }

        public override SearchResultRS GetSearchResult(HDSRequest request)
        {
            shoppingHelper = new ShoppingManager();

            return shoppingHelper.GetSearchResult(request);
        }

        public override HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            shoppingHelper = new ShoppingManager();

            return shoppingHelper.GetHotelAvailability(request);
        }

        public override ReservationRS MakeHotelReservation(HDSRequest request)
        {
            bookingHelper = new BookingManager();

            return bookingHelper.MakeHotelReservation(request);
        }



    }
}
