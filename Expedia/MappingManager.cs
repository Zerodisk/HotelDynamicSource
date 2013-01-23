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
            if (rawRs.moreResultsAvailableSpecified)
                rs.IsMoreResultsAvailable = rawRs.moreResultsAvailable;

            //popuplate each of every hotels
            rs.Hotels = new List<HDSInterfaces.Hotel>();
            foreach (HotelSummary rawHotel in rawRs.HotelList.HotelSummary)
            {
                HDSInterfaces.Hotel hotel = new HDSInterfaces.Hotel();
                hotel.Id = rawHotel.hotelId;
                hotel.Name = rawHotel.name;

                /* ******** for testing ******** */
                if (hotel.Id == 356393){
                    string breakk;
                    breakk = "";
                }
                /* ******** for testing ******** */

                //address and location
                hotel.HotelInfo = new HotelInformation();
                hotel.HotelInfo.Address = new Address{
                                                        Street1 = rawHotel.address1,
                                                        Street2 = rawHotel.address2,
                                                        City = new City { Name = rawHotel.city },
                                                        State = rawHotel.stateProvinceCode,
                                                        Postcode = rawHotel.postalCode,
                                                        Country = new Country { Code = rawHotel.countryCode }
                                                     };
                hotel.HotelInfo.Address.LocationDescription = rawHotel.locationDescription;
                if (rawHotel.latitudeSpecified)
                    hotel.HotelInfo.Address.Latitude  = rawHotel.latitude;
                if (rawHotel.longitudeSpecified)
                    hotel.HotelInfo.Address.Longtitude = rawHotel.longitude;

                //star rating
                if (rawHotel.hotelRatingSpecified)
                    hotel.HotelInfo.StarRating = rawHotel.hotelRating;
                if (rawHotel.tripAdvisorRatingSpecified)
                    hotel.HotelInfo.TripAdvisorRating = rawHotel.tripAdvisorRating;

                //description and image
                hotel.HotelInfo.HotelDescription = rawHotel.shortDescription;
                hotel.HotelInfo.Images = new List<HDSInterfaces.HotelImage>();
                hotel.HotelInfo.Images.Add(new HDSInterfaces.HotelImage { UrlThumbnail = IMAGE_URL_PREFIX + rawHotel.thumbNailUrl });
                
                //rooms and rates
                hotel.Rooms = new List<HDSInterfaces.Room>();
                foreach (RoomRateDetails rawRoom in rawHotel.RoomRateDetailsList)
                {


                    /*
                     * room identification code - do it later
                     */



                    //room info and promotion
                    HDSInterfaces.Room room = new HDSInterfaces.Room();
                    room.Description = rawRoom.roomDescription;
                    room.NumberOfRoomAvailable = rawRoom.currentAllotment;
                    if (!string.IsNullOrEmpty(rawRoom.promoDescription))
                    {
                        room.Promotions = new List<Promotion>();
                        room.Promotions.Add(new Promotion { Code = rawRoom.promoId, Description = rawRoom.promoDescription });
                    }

                    //rate total
                    room.Rates = new RoomRate();
                    room.Rates.TotalRate = new Rate{
                                                     CurrencyCode = rawRoom.RateInfo.ChargeableRateInfo.currencyCode,
                                                     SellRate     = rawRoom.RateInfo.ChargeableRateInfo.total, //this is total include tax
                                                   };
                    if (rawRoom.RateInfo.ChargeableRateInfo.surchargeTotalSpecified)
                        room.Rates.TotalRate.TaxRate = rawRoom.RateInfo.ChargeableRateInfo.surchargeTotal;
                    else
                        room.Rates.TotalRate.TaxRate = 0;

                    //rate daily
                    room.Rates.NightlyRate = new List<Rate>();
                    foreach (NightlyRate rawNightlyRate in rawRoom.RateInfo.ChargeableRateInfo.NightlyRatesPerRoom.NightlyRate)
                    {
                        HDSInterfaces.Rate rate = new Rate{
                                                            SellRate = rawNightlyRate.rate,
                                                            BaseRate = rawNightlyRate.baseRate
                                                          };
                        
                        room.Rates.NightlyRate.Add(rate);
                    }

                    //value adds
                    if (rawRoom.ValueAdds != null)
                    {
                        room.ValueAdds = new List<RoomValueAdd>();
                        foreach (valueAdd rawValueAdd in rawRoom.ValueAdds.ValueAdd)
                        {
                            RoomValueAdd valueAdd = new RoomValueAdd { Id = rawValueAdd.id, Description = rawValueAdd.description };
                            room.ValueAdds.Add(valueAdd);
                        }
                    }

                    hotel.Rooms.Add(room);
                }

                rs.Hotels.Add(hotel);
            }


            return rs;
        }
    }
}
