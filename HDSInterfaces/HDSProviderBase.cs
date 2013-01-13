using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HDSProviderBase : IHDSHotelShopping, IHDSHotelBooking  
    {
        //search by location
        public virtual SearchResultRS GetSearchResultByLocation(HDSRequest request)
        {
            return null;
        }

        //search by hotel
        public virtual HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            return null;
        }

        //rate rules (only for Orbitz)
        public virtual RateRuleRS GetHotelRateRule(HDSRequest request)
        {
            return null;
        }

        //reservation-booking
        public virtual ReservationRS MakeHotelReservation(HDSRequest request)
        {
            return null;
        }






        //Implement IDisposable.
        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (disposing) 
            {
             // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
        }

        // Use C# destructor syntax for finalization code.
        ~HDSProviderBase()
        {
            // Simply call Dispose(false).
            Dispose (false);
        }
    }
}
