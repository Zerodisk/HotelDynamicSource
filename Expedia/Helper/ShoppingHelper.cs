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
        //constant value
        private int DEFAULT_MAX_NUMBER_OF_ROOM = 1; //Defines the number of room types to return with each property.
        
        //private variable
        private Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;
        private CommonHelper commonHelper;
        private MappingShopping objMapping;

        //constructure
        public ShoppingHelper()
        {
            objMapping = new MappingShopping();
            commonHelper = new CommonHelper();
        }

        public SearchResultRS GetSearchResult(HDSRequest request)
        {
            //create hotel list request object(HotelListRequest)
            HotelListRequest rawRq = new HotelListRequest();
            rawRq = (HotelListRequest)commonHelper.GenerateBaseRequest(rawRq, request);

            //set max hotel return
            if (request.Session.PageSize != null)
            {
                rawRq.numberOfResults = (int)request.Session.PageSize;
                rawRq.numberOfResultsSpecified = true;
            }

            //star rating
            if (request.SearchCriteria.MinStarRating != null)
            {
                rawRq.minStarRating = (float)request.SearchCriteria.MinStarRating;
                rawRq.minStarRatingSpecified = true;
            }
            if (request.SearchCriteria.MaxStarRating != null)
            {
                rawRq.maxStarRating = (float)request.SearchCriteria.MaxStarRating;
                rawRq.maxStarRatingSpecified = true;
            }

            //stay date
            rawRq.arrivalDate = request.StayDate.GetCheckInUSFormat();
            rawRq.departureDate = request.StayDate.GetCheckOutUSFormat();

            //room and num adults-children
            rawRq.numberOfBedRooms = request.Itineraries.Count;
            rawRq.maxRatePlanCount = DEFAULT_MAX_NUMBER_OF_ROOM;
            rawRq.RoomGroup = commonHelper.GenerateRoomGroup(request.Itineraries);

            //location keyword or location(s) or list of hotelid
            switch (request.RequestType)
            {
                case HDSRequestType.SearchByHotelIds:
                    int index = 0;
                    rawRq.hotelIdList = new long[request.Hotels.Count];
                    foreach (HDSInterfaces.Hotel hotel in request.Hotels)
                    {
                        rawRq.hotelIdList[index] = (long)hotel.Id;
                        index = index + 1;
                    }

                    break;
                case HDSRequestType.SearchByLocationIds:
                    rawRq.destinationId = request.SearchCriteria.Locations[0].Code;
                    break;
                case HDSRequestType.SearchByLocationKeyword:
                    rawRq.destinationString = request.SearchCriteria.LocationKeyword;
                    break;
            }

            //submit soap request to expedia
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
            HotelListResponse rawRs = serviceObjShop.getList(rawRq);

            return objMapping.MappingSearchResult(rawRs);
        }

        public HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            HotelRoomAvailabilityRequest rawRq = new HotelRoomAvailabilityRequest();
            rawRq = (HotelRoomAvailabilityRequest)commonHelper.GenerateBaseRequest(rawRq, request);

            //stay date
            rawRq.arrivalDate = request.StayDate.GetCheckInUSFormat();
            rawRq.departureDate = request.StayDate.GetCheckOutUSFormat();

            //hotel
            rawRq.hotelId = (long)request.Hotels[0].Id;

            //room and num adults-children
            rawRq.RoomGroup = commonHelper.GenerateRoomGroup(request.Itineraries);

            //include details
            rawRq.includeDetails = true;
            rawRq.includeDetailsSpecified = true;
            rawRq.includeRoomImages = true;
            rawRq.includeRoomImagesSpecified = true;

            //submit soap request to expedia
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
            HotelRoomAvailabilityResponse rawRs = serviceObjShop.getAvailability(rawRq);

            return objMapping.MappingHotelAvailability(rawRs);
        }


    }
}
