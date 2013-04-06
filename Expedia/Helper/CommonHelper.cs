using System;
using System.Collections.Generic;
using System.Text;

using HDSInterfaces;
using Expedia.HotelShoppingServiceReference;

namespace Expedia
{
    public class CommonHelper
    {
        //constant value
        private Expedia.HotelShoppingServiceReference.LocaleType DEFAULT_LOCALE = LocaleType.en_US;

        /*
         * these are list of function to generate Expedia object base of given input parameter
         * 
         * 
         */ 

        //create all the neccessary property for expedia general request parameter
        public Expedia.HotelShoppingServiceReference.HotelBaseRequest GenerateBaseRequest(HotelBaseRequest hotelRequest, HDSRequest request)
        {
            //will need to get apiKey and cid from database base on "request.Session.SiteId"
            hotelRequest.apiKey = "nt2cqy75cmqumtjm2pscc7py";
            hotelRequest.cid = 55505;
            
            hotelRequest.customerIpAddress = request.Session.CustomerIpAddress;
            hotelRequest.customerSessionId = request.Session.SessionId;
            hotelRequest.customerUserAgent = request.Session.BrowserUserAgent;

            if (request.Session.Expedia == null)
            {
                //only setup locale+currency if there is no cacheKey and no cacheLocation
                hotelRequest.currencyCode = request.Session.CurrencyCode;
                hotelRequest.locale = this.MapLocale(request.Session.Locale);
                hotelRequest.localeSpecified = true;
            }

            return hotelRequest;
        }

        public Expedia.HotelShoppingServiceReference.Room[] GenerateRoomGroup(List<HDSInterfaces.Itinerary> itineraries)
        {
            Expedia.HotelShoppingServiceReference.Room[] roomGroup = new Expedia.HotelShoppingServiceReference.Room[itineraries.Count];
            int index = 0;
            foreach (HDSInterfaces.Itinerary itinerary in itineraries)
            {
                Expedia.HotelShoppingServiceReference.Room room = new Expedia.HotelShoppingServiceReference.Room();
                room.numberOfAdults = itinerary.GetNumberOfAdult();
                room.numberOfChildren = itinerary.GetNumberOfChildren();

                if (room.numberOfChildren > 0)
                    room.childAges = itinerary.GetChildAges();

                roomGroup[index] = room;
                index = index + 1;
            }

            return roomGroup;
        }

        /*
         * these are list of function to generate internal HDSInterface object
         * 
         * 
         * 
         */

        public HDSInterfaces.Address GenerateHotelAddress(HotelSummary rawHotel)
        {
            Address address = new Address{
                                            Street1             = rawHotel.address1,
                                            Street2             = rawHotel.address2,
                                            City                = new City { Name = rawHotel.city },
                                            State               = rawHotel.stateProvinceCode,
                                            Postcode            = rawHotel.postalCode,
                                            Country             = new Country { Code = rawHotel.countryCode },
                                            LocationDescription = rawHotel.locationDescription
                                         };

            if (rawHotel.latitudeSpecified) { address.Latitude = rawHotel.latitude; }
            if (rawHotel.longitudeSpecified) { address.Longtitude = rawHotel.longitude; }

            return address;
        }

        public HDSInterfaces.HotelImage GenerateHotelImage(Expedia.HotelShoppingServiceReference.HotelImage rawImage)
        {
            HDSInterfaces.HotelImage image = new HDSInterfaces.HotelImage{
                                                                            Id           = rawImage.hotelImageId,
                                                                            Description  = rawImage.caption,
                                                                            URL          = rawImage.url,
                                                                            UrlThumbnail = rawImage.thumbnailUrl,
                                                                            Width        = (int)rawImage.width,
                                                                            Height       = (int)rawImage.height,
                                                                         };

            return image;
        }

        public HDSInterfaces.RoomRate GenerateRoomRate(Expedia.HotelShoppingServiceReference.HotelRateInfo rawRate)
        {
            RoomRate roomRate = new RoomRate();
            roomRate.TotalRate = new Rate
            {
                CurrencyCode = rawRate.ChargeableRateInfo.currencyCode,
                SellRate = rawRate.ChargeableRateInfo.total,      //this is total include tax
            };
            //tax total
            if (rawRate.ChargeableRateInfo.surchargeTotalSpecified)
                roomRate.TotalRate.TaxAndServiceFee = rawRate.ChargeableRateInfo.surchargeTotal;

            //rate daily
            roomRate.NightlyRate = new List<Rate>();
            foreach (NightlyRate rawNightlyRate in rawRate.ChargeableRateInfo.NightlyRatesPerRoom.NightlyRate)
            {
                HDSInterfaces.Rate rate = new Rate{
                                                    SellRate            = rawNightlyRate.rate,
                                                    BaseRate            = rawNightlyRate.baseRate,
                                                    CurrencyCode        = roomRate.TotalRate.CurrencyCode,
                                                    IsPromotionIncluded = rawNightlyRate.promo
                                                 };

                roomRate.NightlyRate.Add(rate);
            }

            return roomRate;
        }

        public WarningAndError GenerateWarningAndError(int errorCode, EanWsError rawError)
        {
            return new WarningAndError
            {
                Id                = errorCode,
                Type              = rawError.category.ToString(),
                Message           = rawError.presentationMessage,
                DetailDescription = rawError.verboseMessage
            };
        }


        /*
         * function return mapping locale
         *  map from string local to expedia locale
         */ 
        private Expedia.HotelShoppingServiceReference.LocaleType MapLocale(string locale)
        {
            foreach (Expedia.HotelShoppingServiceReference.LocaleType localType in Enum.GetValues(typeof(Expedia.HotelShoppingServiceReference.LocaleType))){
                if (localType.ToString() == locale) { return localType; }
            }

            //return default if not matched
            return DEFAULT_LOCALE;
        }

        

    }
}
