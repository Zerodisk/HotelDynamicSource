using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class CommonHelper
    {
        //constant value
        private Expedia.HotelShoppingServiceReference.LocaleType DEFAULT_LOCALE = LocaleType.en_US;


        //create all the neccessary property for expedia general request parameter
        public HotelBaseRequest GenerateBaseRequest(HotelBaseRequest hotelRequest, HDSRequest request)
        {
            hotelRequest.apiKey = "nt2cqy75cmqumtjm2pscc7py";
            hotelRequest.cid = 55505;

            hotelRequest.customerIpAddress = request.Session.CustomerIpAddress;
            hotelRequest.customerSessionId = request.Session.SessionId;
            hotelRequest.customerUserAgent = request.Session.BrowserUserAgent;

            hotelRequest.currencyCode = request.Session.CurrencyCode;
            hotelRequest.locale = DEFAULT_LOCALE;

            return hotelRequest;
        }

        

    }
}
