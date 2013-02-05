using System;
using System.Collections.Generic;
using System.Text;

using HDSInterfaces;
using Expedia.HotelBookingServiceReference;

namespace Expedia
{
    public class BookingHelper
    {
        Expedia.HotelBookingServiceReference.HotelServicesClient serviceObjBook;

        public ReservationRS MakeHotelReservation(HDSRequest request)
        {
            serviceObjBook = new Expedia.HotelBookingServiceReference.HotelServicesClient();

            return null;
        }

    }
}
