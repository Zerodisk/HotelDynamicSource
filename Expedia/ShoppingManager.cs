using System;
using System.Collections.Generic;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class ShoppingManager
    {
        //constant value
        private int DEFAULT_MAX_NUMBER_OF_ROOM = 1; //Defines the number of room types to return with each property.
        
        //private variable
        private Expedia.HotelShoppingServiceReference.HotelServicesClient serviceObjShop;
        private CommonHelper commonHelper;
        private MappingShopping objMapping;

        //constructure
        public ShoppingManager()
        {
            objMapping = new MappingShopping();
            commonHelper = new CommonHelper();
        }

        public SearchResultRS GetSearchResult(HDSRequest request)
        {
            //create hotel list request object(HotelListRequest)
            HotelListRequest rawRq = new HotelListRequest();
            rawRq = (HotelListRequest)commonHelper.GenerateBaseRequest(rawRq, request);

            if (request.Session.Expedia != null)
            {
                rawRq.cacheKey = request.Session.Expedia.CacheKey;
                rawRq.cacheLocation = request.Session.Expedia.CacheLocation;
            }
            else
            {

                //set max hotel return
                if (request.Session.PageSize != null)
                {
                    rawRq.numberOfResults = (int)request.Session.PageSize;
                    rawRq.numberOfResultsSpecified = true;
                }

                //star rating
                if (request.SearchCriteria != null)
                {
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
                        rawRq.destinationId = request.SearchCriteria.Location.Code;

                        break;
                    case HDSRequestType.SearchByLocationKeyword:
                        rawRq.destinationString = request.SearchCriteria.LocationKeyword;

                        break;
                }

                //addition filter - by hotel name keyword ( **** NOTE: this is excluded to search by list of hotel ID as expedia will ignore this on this search type ***** )
                if (request.SearchCriteria.HotelNameKeyword != null){
                    rawRq.propertyName = request.SearchCriteria.HotelNameKeyword;
                }

                //addition filter - by amenities (NOTE: tested and it's working with search by location keyword, location id and list of hotel id)
                if (request.SearchCriteria.Amenities != null) {
                    if (request.SearchCriteria.Amenities.Count > 0) {
                        int index = 0;
                        rawRq.amenities = new int[request.SearchCriteria.Amenities.Count];
                        foreach (HDSInterfaces.Amenity amenity in request.SearchCriteria.Amenities) {
                            rawRq.amenities[index] = int.Parse(amenity.Code);
                            index = index + 1;
                        }
                    }
                }

                //additon filter - by property type (e.g. hotel, motel, apartment)
                if (request.SearchCriteria.PropertyTypes != null)
                {
                    if (request.SearchCriteria.PropertyTypes.Count > 0)
                    {
                        int index = 0;
                        rawRq.propertyCategory = new PropertyCategory[request.SearchCriteria.PropertyTypes.Count];
                        foreach (HDSInterfaces.PropertyType propertyType in request.SearchCriteria.PropertyTypes)
                        {
                            PropertyCategory pc = PropertyCategory.Item0;
                            switch (propertyType.Code.Trim())
                            {
                                case "1":
                                    pc = PropertyCategory.Item1;
                                    break;
                                case "2":
                                    pc = PropertyCategory.Item2;
                                    break;
                                case "3":
                                    pc = PropertyCategory.Item3;
                                    break;
                                case "4":
                                    pc = PropertyCategory.Item4;
                                    break;
                                case "5":
                                    pc = PropertyCategory.Item5;
                                    break;
                                case "6":
                                    pc = PropertyCategory.Item6;
                                    break;
                            }
                            rawRq.propertyCategory[index] = pc;
                            index = index + 1;
                        }
                    }
                }




            }

            //submit soap request to expedia
            HotelListResponse rawRs;
            try
            {
                serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
                rawRs = serviceObjShop.getList(rawRq);
            }
            catch (Exception e1)
            {
                SearchResultRS error = new SearchResultRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9003, Message = "Error return from provider", DetailDescription = e1.ToString() });
                return error;
            }

            try
            {
                return objMapping.MappingSearchResult(rawRs, request);
            }
            catch (Exception e2)
            {
                SearchResultRS error = new SearchResultRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9110, Message = "Search Result mapping exception", DetailDescription = e2.ToString() });
                return error;
            }
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
            HotelRoomAvailabilityResponse rawRs;
            try
            {
                serviceObjShop = new Expedia.HotelShoppingServiceReference.HotelServicesClient();
                rawRs = serviceObjShop.getAvailability(rawRq);
            }
            catch (Exception e1)
            {
                HotelAvailabilityRS error = new HotelAvailabilityRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9003, Message = "Error return from provider", DetailDescription = e1.ToString() });
                return error;
            }

            //do hotel availability mapping
            try
            {
                return objMapping.MappingHotelAvailability(rawRs);
            }
            catch (Exception e2)
            {
                HotelAvailabilityRS error = new HotelAvailabilityRS();
                error.Errors = new List<WarningAndError>();
                error.Errors.Add(new WarningAndError { Id = 9120, Message = "Hotel Availability mapping exception", DetailDescription = e2.ToString() });
                return error;
            }
        }


    }
}
