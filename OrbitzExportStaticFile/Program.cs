using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HDSInterfaces;

namespace OrbitzExportStaticFile
{
    class Program
    {
        static void Main(string[] args)
        {
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

            Console.Write(output.ObjectToJson(hotel));
            Console.ReadLine();


        }

        /// <summary>
        /// Serializes a class to xml text
        /// </summary>
        /// <param name="item">Class to be converted to XML</param>
        /// <returns>XML string representing class</returns>
        public static string SerializeObjectToXML(object item)
        {
            try
            {
                string xmlText;
                //Get the type of the object
                Type objectType = item.GetType();
                //create serializer object based on the object type
                XmlSerializer xmlSerializer = new XmlSerializer(objectType);
                //Create a memory stream handle the data
                MemoryStream memoryStream = new MemoryStream();
                //Create an XML Text writer to serialize data to
                using (XmlTextWriter xmlTextWriter =
                    new XmlTextWriter(memoryStream, Encoding.UTF8) { Formatting = Formatting.Indented })
                {
                    //convert the object to xml data
                    xmlSerializer.Serialize(xmlTextWriter, item);
                    //Get reference to memory stream
                    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                    //Convert memory byte array into xml text
                    xmlText = new UTF8Encoding().GetString(memoryStream.ToArray());
                    //clean up memory stream
                    memoryStream.Dispose();
                    return xmlText;
                }
            }
            catch (Exception e)
            {
                //There are a number of reasons why this function may fail
                //usually because some of the data on the class cannot
                //be serialized.
                System.Diagnostics.Debug.Write(e.ToString());
                return null;
            }
        }
    }
}
