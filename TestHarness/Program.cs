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
            /*
            SourceSelectionFactory factory = new SourceSelectionFactory();

            IHDSHotelShopping shoppingObj = factory.CreateSourceShopping(HDSSource.Expedia);     //call Expedia
            shoppingObj.GetSearchResultByLocation(null);

            IHDSHotelBooking bookingObj = factory.CreateSourceBooking(HDSSource.Orbitz);
            bookingObj.GetHotelRateRule(null);                                                  //call base method as expedia doesn't have RateRules
            bookingObj.MakeHotelReservation(null);                                              //call expedia
            */

            HDSRequest rq = new HDSRequest();
            rq.CheckIn = DateTime.Now;
            rq.RequestType = HDSRequestType.Reservation;
            
            rq.Session.UserAccess = "Zerodisk";
            rq.Session.CustomerIpAddress = "192.168.1.254";

            rq.Hotels = new List<Hotel>();
            Hotel aHotel = new Hotel();
            aHotel.Name = "Hilton";
            rq.Hotels.Add(aHotel);
            rq.Itineraries = new List<Itinerary>();

            Console.WriteLine(objectToJson(rq));


            Console.Read();
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
