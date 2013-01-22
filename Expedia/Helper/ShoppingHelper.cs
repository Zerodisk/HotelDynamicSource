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
        private Expedia.HotelShoppingServiceReference.LocaleType DEFAULT_LOCALE = LocaleType.en_US;

        //private variable
        private Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;

        //constructure
        public ShoppingHelper()
        {
            serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
        }

        public SearchResultRS GetSearchResult(HDSRequest request)
        {
            //create hotel list request object(HotelListRequest)
            HotelListRequest hotelListRequest = new HotelListRequest();
            hotelListRequest = (HotelListRequest)this.GenerateBaseRequest(hotelListRequest, request);

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
                        hotelListRequest.hotelIdList[index] = hotel.Id;
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





            return new SearchResultRS();
        }

        public HotelAvailabilityRS GetHotelAvailability(HDSRequest request)
        {
            




            return null;
        }

        //create all the neccessary property for expedia general request parameter
        private HotelBaseRequest GenerateBaseRequest(HotelListRequest hotelRequest, HDSRequest request)
        {
            hotelRequest.apiKey = "nt2cqy75cmqumtjm2pscc7py";
            hotelRequest.cid    = 55505;

            hotelRequest.customerIpAddress = request.Session.CustomerIpAddress;
            hotelRequest.customerSessionId = request.Session.SessionId;
            hotelRequest.customerUserAgent = request.Session.BrowserUserAgent;
            
            hotelRequest.currencyCode = request.Session.CurrencyCode;
            hotelRequest.locale = DEFAULT_LOCALE;

            return hotelRequest;
        }
    }
}
