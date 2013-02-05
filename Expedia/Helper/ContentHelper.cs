using System;
using System.Collections.Generic;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class ContentHelper
    {
        //private variable
        private Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;
        private CommonHelper commonHelper;
        private MappingContent objMapping;

        public ContentHelper()
        {
            commonHelper = new CommonHelper();
            objMapping = new MappingContent();
        }

        public HotelContentRS GetHotelInfo(HDSRequest request)
        {
            HotelInformationRequest hotelInfoRequest = new HotelInformationRequest();
            hotelInfoRequest = (HotelInformationRequest)commonHelper.GenerateBaseRequest(hotelInfoRequest, request);

            hotelInfoRequest.hotelId = (long)request.Hotels[0].Id;
            hotelInfoRequest.options = new hotelInfoOption[4];
            hotelInfoRequest.options[0] = new hotelInfoOption();
            hotelInfoRequest.options[0] = hotelInfoOption.HOTEL_DETAILS;        //hotel details
            hotelInfoRequest.options[1] = new hotelInfoOption();
            hotelInfoRequest.options[1] = hotelInfoOption.PROPERTY_AMENITIES;   //hotel amenities
            hotelInfoRequest.options[2] = new hotelInfoOption();
            hotelInfoRequest.options[2] = hotelInfoOption.HOTEL_IMAGES;         //hotel images
            hotelInfoRequest.options[3] = new hotelInfoOption();
            hotelInfoRequest.options[3] = hotelInfoOption.HOTEL_SUMMARY;        //hotel name and address

            //submit soap request to expedia
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
            HotelInformationResponse hotelInfoResponse = serviceObjShop.getInformation(hotelInfoRequest);

            return objMapping.MappingHotelInfo(hotelInfoResponse);
        }

    }
}
