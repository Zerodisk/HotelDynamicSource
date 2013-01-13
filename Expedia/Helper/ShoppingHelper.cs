using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class ShoppingHelper
    {
        Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;

        public ShoppingHelper()
        {
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
        }

        public SearchResultRS GetSearchResultByLocation(HDSRequest request)
        {
            //create hotel list request object(HotelListRequest)
            HotelListRequest hotelListRequest = new HotelListRequest();
            hotelListRequest = (HotelListRequest)this.GenerateBaseRequest(hotelListRequest, request);







            return null;
        }

        public HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            




            return null;
        }

        private HotelBaseRequest GenerateBaseRequest(HotelListRequest hotelListRequest, HDSRequest request)
        {
            hotelListRequest.apiKey = "testtest";
            //hotelListRequest.cid = 0;

            hotelListRequest.customerIpAddress = request.Session.CustomerIpAddress;
            //hotelListRequest.customerSessionId = request.Session.SessionId;
            hotelListRequest.customerUserAgent = request.Session.BrowserUserAgent;
            
            //hotelListRequest.locale = LocaleType
            hotelListRequest.currencyCode = request.Session.CurrencyCode;

            return hotelListRequest;
        }
    }
}
