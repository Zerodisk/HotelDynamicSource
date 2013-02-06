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
            HotelInformationResponse hotelInfoResponse;
            try
            {
                serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
                hotelInfoResponse = serviceObjShop.getInformation(hotelInfoRequest);                
            }
            catch (Exception e1)
            {
                HotelContentRS error = new HotelContentRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9003, Message = "Error return from provider", DetailDescription = e1.ToString() });
                return error;
            }

            //do hotel content object mapping
            try
            {
                return objMapping.MappingHotelInfo(hotelInfoResponse);
            }
            catch(Exception e2)
            {
                HotelContentRS error = new HotelContentRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9150, Message = "Hotel Conent mapping exception", DetailDescription = e2.ToString() });
                return error;
            }
            
        }

    }
}
