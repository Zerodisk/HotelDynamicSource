using System;
using System.Collections.Generic;
using System.Linq;
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

            //amenitity
            hotel.HotelInfo.Amenities = new List<HDSInterfaces.Amenity>();
            if (rawRs.PropertyAmenities != null){
                foreach (PropertyAmenity rawAmenitity in rawRs.PropertyAmenities.PropertyAmenity)
                {
                    HDSInterfaces.Amenity amenity = new Amenity { Id = rawAmenitity.amenityId, Description = rawAmenitity.amenity.Trim() };
                    hotel.HotelInfo.Amenities.Add(amenity);
                }
            }

            //images
            hotel.HotelInfo.Images = new List<HDSInterfaces.HotelImage>();
            if (rawRs.HotelImages != null){
                foreach (Expedia.HotelShoppingServiceReference.HotelImage rawImage in rawRs.HotelImages.HotelImage)
                {                   
                    hotel.HotelInfo.Images.Add(helper.GenerateHotelImage(rawImage));
                }
            }

            rs.Hotels.Add(hotel);
            return rs;
        }
    }
}
