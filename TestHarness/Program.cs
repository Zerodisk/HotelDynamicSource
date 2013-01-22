using System;
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
            rq.StayDate.CheckIn = "2013-01-02";
            
            rq.Session.UserAccess = "Zerodisk";
            rq.Session.CustomerIpAddress = "192.168.1.254";

            rq.SearchCriteria = new SearchCriteria();
            rq.SearchCriteria.LocationKeyword = "Osaka";

            rq.Hotels = new List<Hotel>();
            Hotel aHotel = new Hotel();
            aHotel.Id = 96768574;
            aHotel.Name = "Hilton";
            rq.Hotels.Add(aHotel);
            rq.Itineraries = new List<Itinerary>();
            Itinerary itinerary = new Itinerary(2, "1_2");
            rq.Itineraries.Add(itinerary);


            Console.WriteLine(objectToJson(rq));

            IHDSHotelShopping shoppingObj = factory.CreateSourceShopping(HDSSource.Expedia);     //call Expedia
            SearchResultRS rs  = shoppingObj.GetSearchResult(rq);

            Console.ReadLine();

            Console.WriteLine("\n------------------------------\n");
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
