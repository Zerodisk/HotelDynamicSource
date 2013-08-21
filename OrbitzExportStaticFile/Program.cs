using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDSInterfaces;


namespace OrbitzExportStaticFile
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            OutputFormatter output = new OutputFormatter();

            HotelInformation hotelInfo = new HotelInformation();
            hotelInfo.Amenities = new List<Amenity>();

            Amenity a = new Amenity();
            a.Code = "1234";
            a.Description = "Test A";

            Amenity b = new Amenity();
            b.Code = "4567";
            b.Description = "Test B";

            Amenity c = new Amenity();
            c.Code = "5678";
            c.Description = "Test C";

            hotelInfo.Amenities.Add(a);
            hotelInfo.Amenities.Add(b);
            hotelInfo.Amenities.Add(c);
            hotelInfo.Id = 191;
            hotelInfo.Name = "Tan Hotel";
            hotelInfo.StarRating = (float)0.123456789d;

            hotelInfo.RoomInfos = new List<RoomInfo>();
            hotelInfo.RoomInfos.Add(new RoomInfo { Name = "test 1" });
            hotelInfo.RoomInfos.Add(new RoomInfo { Name = "test 2" });
            hotelInfo.RoomInfos.Add(new RoomInfo { Name = "test 3" });

            Hotel hotel = new Hotel();
            hotel.HotelInfo = hotelInfo;
            string jsonString = output.ObjectToJson(hotel);

            //display
            Console.Write(jsonString);
            //to file
            output.WriteToFile(jsonString, "c:\\temp\\1.json");
            */

            Console.WriteLine("Starting app");
            ExportManager manager = new ExportManager();
            manager.DoStart();
            Console.ReadLine();


        }

        
    }
}
