﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelDynamicSource;
using HDSInterfaces;
using Newtonsoft.Json;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SourceSelectionFactory factory = new SourceSelectionFactory();

            /*
            IHDSHotelBooking bookingObj = factory.CreateSourceBooking(HDSSource.Orbitz);
            bookingObj.GetHotelRateRule(null);                                                  //call base method as expedia doesn't have RateRules
            bookingObj.MakeHotelReservation(null);                                              //call expedia
            */

            HDSRequest rq = new HDSRequest(HDSRequestType.SearchByLocationKeyword);
            rq.StayDate = new StayDate();
            rq.StayDate.CheckIn  = "2013-03-17";
            rq.StayDate.CheckOut = "2013-03-19";

            rq.Session.CurrencyCode = "AUD";
            rq.Session.UserAccess = "Zerodisk";
            rq.Session.CustomerIpAddress = "203.1.2.3";
            rq.Session.BrowserUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/22.0.1207.1 Safari/537.1";

            rq.SearchCriteria = new SearchCriteria();
            rq.SearchCriteria.LocationKeyword = "New York";
            //rq.SearchCriteria.Locations = new List<Location>();
            //rq.SearchCriteria.Locations.Add(new Location { Code = "B0055425-19CE-4D8F-8769-A2DB23ED2E46" });

            rq.SearchCriteria.MinStarRating = 4;
            rq.SearchCriteria.MaxStarRating = 5;

            /*
            rq.Hotels = new List<Hotel>();
            Hotel aHotel = new Hotel();
            aHotel.Id = 96768574;
            aHotel.Name = "Hilton";
            rq.Hotels.Add(aHotel);
            */

            rq.Itineraries = new List<Itinerary>();
            Itinerary itinerary = new Itinerary(1, "1_2");
            rq.Itineraries.Add(itinerary);


            Console.WriteLine(objectToJson(rq));
            Console.WriteLine("\n------------------------------\n");
            Console.Write("Press enter key to continue");
            Console.ReadLine();
            Console.Write("..\n");
            

            IHDSHotelShopping shoppingObj = factory.CreateSourceShopping(HDSSource.Expedia);     //call Expedia
            SearchResultRS rs  = shoppingObj.GetSearchResult(rq);

            
            Console.WriteLine(objectToJson(rs));
            Console.ReadLine();
        }

        static string objectToJson(object obj)
        {
            /*
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string output = oSerializer.Serialize(obj);
            */

            
            string output = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            

            return output;
        }
    }
}
