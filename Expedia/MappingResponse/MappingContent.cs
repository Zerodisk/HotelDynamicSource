using System;
using System.Collections.Generic;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class MappingContent
    {
        private CommonHelper helper = new CommonHelper();

        public HotelContentRS MappingHotelInfo(HotelInformationResponse rawRs)
        {
            HotelContentRS rs = new HotelContentRS();

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

                rs.Hotels = new List<HDSInterfaces.Hotel>();

                HDSInterfaces.Hotel hotel = new HDSInterfaces.Hotel();
                hotel.HotelInfo = new HotelInformation();
                hotel.Id = rawRs.hotelId;

                //name and address
                if (rawRs.HotelSummary != null){
                    hotel.Name = rawRs.HotelSummary.name;
                    hotel.HotelInfo.Address = helper.GenerateHotelAddress(rawRs.HotelSummary);

                    //rating
                    if (rawRs.HotelSummary.hotelRatingSpecified) { hotel.HotelInfo.StarRating = rawRs.HotelSummary.hotelRating; }
                    if (rawRs.HotelSummary.tripAdvisorRatingSpecified) { hotel.HotelInfo.TripAdvisorRating = rawRs.HotelSummary.tripAdvisorRating; }
                }

                //description and information
                if (rawRs.HotelDetails != null){
                    hotel.HotelInfo.HotelDescription = rawRs.HotelDetails.propertyDescription;
                    hotel.HotelInfo.AreaInfo         = rawRs.HotelDetails.areaInformation;
                    hotel.HotelInfo.RoomInfo         = rawRs.HotelDetails.roomInformation;
                    hotel.HotelInfo.DrivingDirection = rawRs.HotelDetails.drivingDirections;
                    hotel.HotelInfo.AdditionalInfo   = rawRs.HotelDetails.propertyInformation;

                    hotel.HotelInfo.PolicyInfo = new HotelPolicy{
                                                                    PolicyDescription  = rawRs.HotelDetails.hotelPolicy,
                                                                    CheckInInstruction = rawRs.HotelDetails.checkInInstructions,
                                                                    CheckInTime        = rawRs.HotelDetails.checkInTime,
                                                                    CheckOutTime       = rawRs.HotelDetails.checkOutTime,
                                                                };
                }

                //room info
                hotel.HotelInfo.RoomInfos = new List<RoomInformation>();
                if (rawRs.RoomTypes != null) {
                    if (rawRs.RoomTypes.size > 0){
                        foreach (Expedia.HotelShoppingServiceReference.RoomType roomType in rawRs.RoomTypes.RoomType){
                            RoomInformation room = new RoomInformation{
                                                                         Id = roomType.roomTypeId,
                                                                         Code = roomType.roomCode,
                                                                         Name = roomType.description,
                                                                         Description = roomType.descriptionLong
                                                                      };
                            hotel.HotelInfo.RoomInfos.Add(room);
                        }
                    }
                }

                //amenitity
                hotel.HotelInfo.Amenities = new List<HDSInterfaces.Amenity>();
                if (rawRs.PropertyAmenities != null){
                    if (rawRs.PropertyAmenities.size > 0){
                        foreach (PropertyAmenity rawAmenitity in rawRs.PropertyAmenities.PropertyAmenity){
                            HDSInterfaces.Amenity amenity = new Amenity { Id = rawAmenitity.amenityId, Description = rawAmenitity.amenity.Trim() };
                            hotel.HotelInfo.Amenities.Add(amenity);
                        }
                    }
                }

                //images
                hotel.HotelInfo.Images = new List<HDSInterfaces.HotelImage>();
                if (rawRs.HotelImages != null){
                    if (rawRs.HotelImages.size > 0){
                        foreach (Expedia.HotelShoppingServiceReference.HotelImage rawImage in rawRs.HotelImages.HotelImage){
                            hotel.HotelInfo.Images.Add(helper.GenerateHotelImage(rawImage));
                        }
                    }
                }

                rs.Hotels.Add(hotel);
            }


            return rs;
        }
    }
}
