using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class MappingManager
    {
        private string IMAGE_URL_PREFIX = "http://images.travelnow.com";

        public SearchResultRS MappingSearchResult(HotelListResponse rawRs)
        {
            SearchResultRS rs = new SearchResultRS();

            //EAN warning and error
            if (rawRs.EanWsError != null)
            {
                if ((rawRs.EanWsError.handling == errorHandling.RECOVERABLE) && (rawRs.EanWsError.category == errorCategory.DATA_VALIDATION)){
                    //warning about location
                    rs.Warnings = new List<WarningAndError>();
                    WarningAndError warning = new WarningAndError {
                                                                     Type              = rawRs.EanWsError.category.ToString(),
                                                                     Message           = rawRs.EanWsError.presentationMessage,
                                                                     DetailDescription = rawRs.EanWsError.verboseMessage
                                                                  };
                    rs.Warnings.Add(warning);
                }
                else{
                    //error! something has happened
                    rs.Errors = new List<WarningAndError>();
                    WarningAndError error = new WarningAndError{
                                                                 Type = rawRs.EanWsError.category.ToString(),
                                                                 Message           = rawRs.EanWsError.presentationMessage,
                                                                 DetailDescription = rawRs.EanWsError.verboseMessage
                                                               };
                    rs.Errors.Add(error);
                }
            }

            //hotels
            if (rawRs.HotelList != null)
            {
                if (rawRs.moreResultsAvailableSpecified)
                    rs.IsMoreResultsAvailable = rawRs.moreResultsAvailable;

                //popuplate each of every hotels
                rs.Hotels = new List<HDSInterfaces.Hotel>();
                foreach (HotelSummary rawHotel in rawRs.HotelList.HotelSummary)
                {
                    bool isHotelWithRate = false;

                    HDSInterfaces.Hotel hotel = new HDSInterfaces.Hotel();
                    hotel.Id = rawHotel.hotelId;
                    hotel.Name = rawHotel.name;

                    /* ****************** for testing ****************** */
                    if (hotel.Id == 356393) { string breakkkkkkk = ""; }
                    /* ****************** for testing ****************** */

                    //address and location
                    hotel.HotelInfo = new HotelInformation();
                    hotel.HotelInfo.Address = new Address{
                                                            Street1  = rawHotel.address1,
                                                            Street2  = rawHotel.address2,
                                                            City     = new City { Name = rawHotel.city },
                                                            State    = rawHotel.stateProvinceCode,
                                                            Postcode = rawHotel.postalCode,
                                                            Country  = new Country { Code = rawHotel.countryCode }
                                                         };
                    hotel.HotelInfo.Address.LocationDescription = rawHotel.locationDescription;
                    if (rawHotel.latitudeSpecified)  { hotel.HotelInfo.Address.Latitude   = rawHotel.latitude;  }
                    if (rawHotel.longitudeSpecified) { hotel.HotelInfo.Address.Longtitude = rawHotel.longitude; }

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

                            //rate total
                            room.Rates = new RoomRate();
                            room.Rates.TotalRate = new Rate{
                                                            CurrencyCode = rawRoom.RateInfo.ChargeableRateInfo.currencyCode,
                                                            SellRate = rawRoom.RateInfo.ChargeableRateInfo.total,      //this is total include tax
                                                       };
                            //tax total
                            if (rawRoom.RateInfo.ChargeableRateInfo.surchargeTotalSpecified)
                                room.Rates.TotalRate.TaxaRate = rawRoom.RateInfo.ChargeableRateInfo.surchargeTotal;

                            //rate daily
                            room.Rates.NightlyRate = new List<Rate>();
                            foreach (NightlyRate rawNightlyRate in rawRoom.RateInfo.ChargeableRateInfo.NightlyRatesPerRoom.NightlyRate){
                                HDSInterfaces.Rate rate = new Rate{
                                                                    SellRate = rawNightlyRate.rate,
                                                                    BaseRate = rawNightlyRate.baseRate,
                                                                    CurrencyCode = room.Rates.TotalRate.CurrencyCode,
                                                                    IsPromotionIncluded = rawNightlyRate.promo
                                                              };

                                room.Rates.NightlyRate.Add(rate);
                            }

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
                        Location location = new Location{
                                                            Code = rawLocation.destinationId,
                                                            Name = rawLocation.code,
                                                            Address = new Address{
                                                                                    State   = rawLocation.stateProvinceCode,
                                                                                    City    = new City { Name = rawLocation.city },
                                                                                    Country = new Country { Name = rawLocation.countryName, Code = rawLocation.countryCode },
                                                                                 }
                                                        };
                        rs.Locations.Add(location);
                    }
                }
            }

            return rs;
        }
    }
}
