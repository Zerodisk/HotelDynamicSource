using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class MappingShopping
    {
        private string IMAGE_URL_PREFIX = "http://images.travelnow.com";
        private CommonHelper helper = new CommonHelper();

        public SearchResultRS MappingSearchResult(HotelListResponse rawRs, HDSRequest request)
        {
            SearchResultRS rs = new SearchResultRS();

            //EAN warning and error
            if (rawRs.EanWsError != null)
            {
                if ((rawRs.EanWsError.handling == errorHandling.RECOVERABLE) && (rawRs.EanWsError.category == errorCategory.DATA_VALIDATION)){
                    //warning about location
                    rs.Warnings = new List<WarningAndError>();
                    WarningAndError warning = helper.GenerateWarningAndError(9002, rawRs.EanWsError);
                    rs.Warnings.Add(warning);
                }
                else{
                    //error! something has happened
                    rs.Errors = new List<WarningAndError>();
                    WarningAndError error = helper.GenerateWarningAndError(9001, rawRs.EanWsError);
                    rs.Errors.Add(error);
                }
            }

            //hotels
            if (rawRs.HotelList != null)
            {
                //pagination
                if (rawRs.moreResultsAvailableSpecified){
                    rs.IsMoreResultsAvailable = rawRs.moreResultsAvailable;
                    if (rawRs.moreResultsAvailable){
                        string tempQs = request.RequestQueryString;

                        if (tempQs.Contains("&cacheKey=") && request.Session.Expedia != null)         //remove old cacheKey from request query string
                            tempQs = tempQs.Replace("&cacheKey=" + HttpUtility.UrlEncode(request.Session.Expedia.CacheKey), "");

                        if (tempQs.Contains("&cacheLocation=") && request.Session.Expedia != null)    //remove old cacheLocation from request query string
                            tempQs = tempQs.Replace("&cacheLocation=" + HttpUtility.UrlEncode(request.Session.Expedia.CacheLocation), "");

                        rs.NextPageQueryString = tempQs + "&cacheKey="      + HttpUtility.UrlEncode(rawRs.cacheKey)
                                                        + "&cacheLocation=" + HttpUtility.UrlEncode(rawRs.cacheLocation);

                        //re-create new expedia session
                        rs.Session.Expedia = new ExpediaSpecific { CacheKey = rawRs.cacheKey, CacheLocation = rawRs.cacheLocation };
                    }

                }

                //popuplate each of every hotels
                rs.Hotels = new List<HDSInterfaces.Hotel>();
                foreach (HotelSummary rawHotel in rawRs.HotelList.HotelSummary)
                {
                    bool isHotelWithRate = false;

                    HDSInterfaces.Hotel hotel = new HDSInterfaces.Hotel();
 
                    /* ****************** for testing ****************** */
                    //if (hotel.Id == 356393) { string breakkkkkkk = ""; }
                    /* ****************** for testing ****************** */

                    //address and location
                    hotel.HotelInfo = new HotelInformation();
                    hotel.HotelInfo.Id = rawHotel.hotelId;
                    hotel.HotelInfo.Name = rawHotel.name;

                    hotel.HotelInfo.Address = helper.GenerateHotelAddress(rawHotel);                   

                    //star rating
                    if (rawHotel.hotelRatingSpecified) { hotel.HotelInfo.StarRating = rawHotel.hotelRating; }
                    if (rawHotel.tripAdvisorRatingSpecified) { hotel.HotelInfo.TripAdvisorRating = rawHotel.tripAdvisorRating; }

                    //description and image
                    hotel.HotelInfo.HotelDescription = rawHotel.shortDescription;
                    hotel.HotelInfo.Images = new List<HDSInterfaces.HotelImage>();
                    hotel.HotelInfo.Images.Add(new HDSInterfaces.HotelImage { UrlThumbnail = IMAGE_URL_PREFIX + rawHotel.thumbNailUrl });

                    //rooms and rates - some expedia hotel won't have rate (but still has all other information)
                    if (rawHotel.RoomRateDetailsList != null)
                    {
                        isHotelWithRate = true;
                        hotel.Rooms = new List<HDSInterfaces.Room>();
                        foreach (RoomRateDetails rawRoom in rawHotel.RoomRateDetailsList)
                        {


                            /*
                             * room identification code - do it later
                             */



                            //room info and promotion
                            HDSInterfaces.Room room = new HDSInterfaces.Room();
                            room.Description = rawRoom.roomDescription;
                            if (rawRoom.currentAllotment > 0) { room.NumberOfRoomAvailable = rawRoom.currentAllotment; }    //as per expedia document, 0 doesn't mean unavailable :(

                            //promotion
                            if (!string.IsNullOrEmpty(rawRoom.promoDescription)) {
                                room.Promotions = new List<Promotion>();
                                room.Promotions.Add(new Promotion { Code = rawRoom.promoId, Description = rawRoom.promoDescription });
                            }
                            
                            //room rate total and nightly
                            room.Rates = helper.GenerateRoomRate(rawRoom.RateInfo);

                            //value adds
                            if (rawRoom.ValueAdds != null){
                                room.ValueAdds = new List<RoomValueAdd>();
                                foreach (valueAdd rawValueAdd in rawRoom.ValueAdds.ValueAdd){
                                    RoomValueAdd valueAdd = new RoomValueAdd { Id = rawValueAdd.id, Description = rawValueAdd.description };
                                    room.ValueAdds.Add(valueAdd);
                                }
                            }

                            hotel.Rooms.Add(room);
                        }

                    }

                    if (isHotelWithRate)        //populate hotel only if there is at least 1 rate
                        rs.Hotels.Add(hotel);
                }
            }

            //location suggestion
            if (rawRs.LocationInfos != null){
                rs.Locations = new List<Location>();
                foreach (LocationInfo rawLocation in rawRs.LocationInfos.LocationInfo) {
                    if (rawLocation.active){
                        Location location = new Location
                        {
                            Code = rawLocation.destinationId,
                            Name = rawLocation.code,
                            Address = new Address
                            {
                                State = new State { Name = rawLocation.stateProvinceCode },
                                City = new City { Name = rawLocation.city },
                                Country = new Country { Name = rawLocation.countryName, Code = rawLocation.countryCode },
                            }
                        };
                        rs.Locations.Add(location);
                    }
                }
            }

            return rs;
        }


        public HotelAvailabilityRS MappingHotelAvailability(HotelRoomAvailabilityResponse rawRs)
        {
            HotelAvailabilityRS rs = new HotelAvailabilityRS();

            //EAN warning and error
            if (rawRs.EanWsError != null)
            {
                //error! something has happened
                rs.Errors = new List<WarningAndError>();
                WarningAndError error = helper.GenerateWarningAndError(9001, rawRs.EanWsError);
                rs.Errors.Add(error);
            }
            else
            {

                //init hotel
                rs.Hotel = new HDSInterfaces.Hotel();

                //hotel address info
                rs.Hotel.HotelInfo = new HotelInformation();
                rs.Hotel.HotelInfo.Id = rawRs.hotelId;
                rs.Hotel.HotelInfo.Name = rawRs.hotelName;
                rs.Hotel.HotelInfo.Address = new Address();
                rs.Hotel.HotelInfo.Address.Street1 = rawRs.hotelAddress;
                rs.Hotel.HotelInfo.Address.City = new City { Name = rawRs.hotelCity };
                rs.Hotel.HotelInfo.Address.Country = new Country { Code = rawRs.hotelCountry };

                //room
                rs.Hotel.Rooms = new List<HDSInterfaces.Room>();
                foreach (HotelRoomResponse rawRoom in rawRs.HotelRoomResponse)
                {
                    HDSInterfaces.Room room = new HDSInterfaces.Room();

                    //room cancellation
                    room.CancellationPolicy = new CancellationPolicy { CancellationPolicyDescription = rawRoom.cancellationPolicy };
                    room.CancellationPolicy.IsNonRefundable = rawRoom.nonRefundable;

                    //room info
                    room.Name = rawRoom.rateDescription;
                    room.Description = rawRoom.rateDescription;
                    room.RoomInfo = new RoomInformation();

                    //room promotion
                    if (rawRoom.promoDescription != null){
                        room.Promotions = new List<Promotion>();
                        room.Promotions.Add(new Promotion { Code = rawRoom.promoId, Description = rawRoom.promoDescription });
                    }

                    //room bedding config
                    if (rawRoom.BedTypes != null)
                        room.RoomInfo.BeddingDescription = rawRoom.BedTypes.BedType[0].description;

                    //room images
                    if (rawRoom.RoomImages != null){
                        room.RoomInfo.Images = new List<HDSInterfaces.RoomImage>();
                        room.RoomInfo.Images.Add(new HDSInterfaces.RoomImage { URL = rawRoom.RoomImages.RoomImage[0].url });
                    }

                    //value adds
                    if (rawRoom.ValueAdds != null){
                        room.ValueAdds = new List<RoomValueAdd>();
                        foreach (valueAdd rawValueAdd in rawRoom.ValueAdds.ValueAdd){
                            room.ValueAdds.Add(new RoomValueAdd { Id = rawValueAdd.id, Description = rawValueAdd.description });
                        }
                    }

                    //room rate total and nightly
                    room.Rates = helper.GenerateRoomRate(rawRoom.RateInfo);

                    rs.Hotel.Rooms.Add(room);
                }
            }


            return rs;
        }

    }
}
