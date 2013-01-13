using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    //base class interface
    public interface IHDSBase : IDisposable
    {

    }

    //manager class interface
    public interface IHDSManager : IHDSBase 
    {

    }

    //content - hotel description
    public interface IHDSHotelContent : IHDSManager
    {

    }


    //shopping class interface - for serach result and hotel page
    public interface IHDSHotelShopping : IHDSManager
    {
        SearchResultByLocationRS GetSearchResultByLocation(HDSRequest request);         
        /*
         * expedia -> HotelListRequest/Response
         * orbitz  -> HotelShoppingRequest/Response
         */

        HotelAvailabilityRS GetHotelAvailability (HDSRequest request);
        /*
         * expedia -> HotelRoomAvailabilityRequest
         * orbitz  -> HotelShoppingRequest/Response (with a single hotel)
         */ 

    }

    //booking class interface - for raterule(if needed) and booking
    public interface IHDSHotelBooking : IHDSManager
    {
        HotelRateRuleRS GetHotelRateRule (HDSRequest request);
        /*
         * expedia -> doesn't seem to have one
         * orbitz  -> HotelRateRulesRequest/Response
         */ 

        HotelReservationRS MakeHotelReservation (HDSRequest request);
        /*
         * expedia -> HotelRoomReservationRequest/Response
         * orbitz  -> HotelReservationRequest
         */

    }



}
