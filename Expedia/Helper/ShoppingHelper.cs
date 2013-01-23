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
        private int DEFAULT_MAX_NUMBER_OF_ROOM = 2; //Defines the number of room types to return with each property.
        
        //private variable
        private Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;
        private CommonHelper commonHelper;
        private MappingManager objMapping;

        //constructure
        public ShoppingHelper()
        {
            objMapping = new MappingManager();
            commonHelper = new CommonHelper();
        }

        public SearchResultRS GetSearchResult(HDSRequest request)
        {
            //create hotel list request object(HotelListRequest)
            HotelListRequest hotelListRequest = new HotelListRequest();
            hotelListRequest = (HotelListRequest)commonHelper.GenerateBaseRequest(hotelListRequest, request);

            //set max hotel return
            if (request.Session.PageSize != null)
            {
                hotelListRequest.numberOfResults = (int)request.Session.PageSize;
                hotelListRequest.numberOfResultsSpecified = true;
            }

            //star rating
            if (request.SearchCriteria.MinStarRating != null)
            {
                hotelListRequest.minStarRating = (float)request.SearchCriteria.MinStarRating;
                hotelListRequest.minStarRatingSpecified = true;
            }
            if (request.SearchCriteria.MaxStarRating != null)
            {
                hotelListRequest.maxStarRating = (float)request.SearchCriteria.MaxStarRating;
                hotelListRequest.maxStarRatingSpecified = true;
            }

            //stay date
            hotelListRequest.arrivalDate   = request.StayDate.GetCheckInUSFormat();
            hotelListRequest.departureDate = request.StayDate.GetCheckOutUSFormat();

            //room and num adults-children
            hotelListRequest.numberOfBedRooms = request.Itineraries.Count;
            hotelListRequest.maxRatePlanCount = DEFAULT_MAX_NUMBER_OF_ROOM;
            hotelListRequest.RoomGroup = new Expedia.HotelShoppingServiceReference.Room[hotelListRequest.numberOfBedRooms];
            int index = 0;
            foreach (HDSInterfaces.Itinerary itinerary in request.Itineraries)
            {
                Expedia.HotelShoppingServiceReference.Room room = new Expedia.HotelShoppingServiceReference.Room();
                room.numberOfAdults     = itinerary.GetNumberOfAdult();
                room.numberOfChildren   = itinerary.GetNumberOfChildren();
                room.childAges          = itinerary.GetChildAges();

                hotelListRequest.RoomGroup[index] = room;
                index = index + 1;
            }

            //location keyword or location(s) or list of hotelid
            switch (request.RequestType)
            {
                case HDSRequestType.SearchByHotelIds:
                    index = 0;
                    hotelListRequest.hotelIdList = new long[request.Hotels.Count];
                    foreach (HDSInterfaces.Hotel hotel in request.Hotels)
                    {
                        hotelListRequest.hotelIdList[index] = (long)hotel.Id;
                        index = index + 1;
                    }

                    break;
                case HDSRequestType.SearchByLocationIds:
                    hotelListRequest.destinationId = request.SearchCriteria.Locations[0].Code;
                    break;
                case HDSRequestType.SearchByLocationKeyword:
                    hotelListRequest.destinationString = request.SearchCriteria.LocationKeyword;
                    break;
            }

            //submit soap request to expedia
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
            HotelListResponse hotelListResponse = serviceObjShop.getList(hotelListRequest);




            return objMapping.MappingSearchResult(hotelListResponse);
        }

        public HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            




            return null;
        }


    }
}
