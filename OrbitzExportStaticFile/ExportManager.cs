using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HDSInterfaces;

namespace OrbitzExportStaticFile
{
    public class ExportManager
    {
        private OutputFormatter outputFormat = new OutputFormatter();
        private AmenityCode amenitiesList = new AmenityCode();
        private Config config = new Config();

        //main method to export
        public void DoStart()
        {
            string xmlFile = config.InputXmlFile;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);
                int indexHotels = 0;
                //finding hotels node
                for (int i = 0; i <= xmlDoc.ChildNodes.Count - 1; i++)
                {
                    if (xmlDoc.ChildNodes.Item(i).Name == "hotels"){
                        indexHotels = i;
                        break;
                    }
                }

                int count = 0;
                foreach (XmlNode hotelNode in xmlDoc.ChildNodes.Item(indexHotels).ChildNodes)
                {
                    count = count + 1;
                    Console.WriteLine("Read and process hotel# " + count.ToString());
                    Hotel hotel = processOneHotel(hotelNode);

                    if (hotel != null)
                    {
                        string jsonString = outputFormat.ObjectToJson(hotel);
                        long hotelId = (long)hotel.HotelInfo.Id;
                        string outputFile = this.getFullPathFileName(hotelId, "json");

                        if (outputFormat.WriteToFile(jsonString, outputFile)) {
                            Console.WriteLine("  Write file: Success - " + outputFile);
                        }
                        else{
                            Console.WriteLine("  Write file: Failed");
                        }                        
                    }

                    Console.WriteLine("Finishing hotel# " + count.ToString());
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //split orbitz hotel static file into a hotel per file.
        public void DoStart2()
        {
            string xmlFile = config.InputXmlFile;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);
                int indexHotels = 0;
                //finding hotels node
                for (int i = 0; i <= xmlDoc.ChildNodes.Count - 1; i++){
                    if (xmlDoc.ChildNodes.Item(i).Name == "hotels"){
                        indexHotels = i;
                        break;
                    }
                }

                int count = 0;
                foreach (XmlNode hotelNode in xmlDoc.ChildNodes.Item(indexHotels).ChildNodes){

                    count = count + 1;
                    Console.WriteLine("Read and process hotel# " + count.ToString());

                    string xmlString = hotelNode.OuterXml;
                    long hotelId = long.Parse(hotelNode.FirstChild.InnerText);
                    string outputFile = this.getFullPathFileName(hotelId, "xml");
                    if (outputFormat.WriteToFile(xmlString, outputFile))
                    {
                        Console.WriteLine("  Write file: Success - " + outputFile);
                    }
                    else
                    {
                        Console.WriteLine("  Write file: Failed");
                    }

                    Console.WriteLine("Finishing hotel# " + count.ToString());
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //
        //
        public void DoStart3()
        {
            string xmlFile = config.InputXmlFile;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);
                int indexHotels = 0;
                //finding hotels node
                for (int i = 0; i <= xmlDoc.ChildNodes.Count - 1; i++)
                {
                    if (xmlDoc.ChildNodes.Item(i).Name == "hotels")
                    {
                        indexHotels = i;
                        break;
                    }
                }

                int count = 0;
                foreach (XmlNode hotelNode in xmlDoc.ChildNodes.Item(indexHotels).ChildNodes)
                {
                    count = count + 1;
                    //Console.WriteLine("Read and process hotel# " + count.ToString());
                    Hotel hotel = processOneHotel(hotelNode);

                    if (hotel != null)
                    {
                        if (hotel.HotelInfo.Address.State != null)
                        {
                            if ((hotel.HotelInfo.Address.State.Code == "HI") && (hotel.HotelInfo.Address.State.Name == "Hawaii"))
                            {
                                string temp = "";
                                temp = temp + hotel.HotelInfo.Id.ToString();
                                temp = temp + "|" + hotel.HotelInfo.Name;
                                temp = temp + "|" + hotel.HotelInfo.Address.City.Name;
                                temp = temp + "|" + hotel.HotelInfo.Address.Street1;
                                if (hotel.HotelInfo.Address.Street2 == null)
                                {
                                    temp = temp + "|";
                                }
                                else
                                {
                                    temp = temp + "|" + hotel.HotelInfo.Address.Street2;
                                }
                                
                                temp = temp + "|" + hotel.HotelInfo.Address.State.Code;
                                temp = temp + "|" + hotel.HotelInfo.Address.State.Name;

                                if (hotel.HotelInfo.Address.Postcode == null)
                                {
                                    temp = temp + "|";
                                }   
                                else
                                {
                                    temp = temp + "|" + hotel.HotelInfo.Address.Postcode;
                                }

                                Console.WriteLine(temp);
                            }
                        }
                    }

                    //Console.WriteLine("Finishing hotel# " + count.ToString());
                    //Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private Hotel processOneHotel(XmlNode hotelNode)
        {
            try
            {
                Hotel result = new Hotel();
                result.HotelInfo = new HotelInformation();
                result.HotelInfo.Address = new Address();

                foreach (XmlNode node in hotelNode.ChildNodes)
                {

                    if (node.Name == "id") {
                        result.HotelInfo.Id = long.Parse(node.ChildNodes.Item(0).InnerText);
                    }

                    if (node.Name == "name") {
                        result.HotelInfo.Name = node.InnerText;

                        //Console.WriteLine("  Hotel Id: " + result.HotelInfo.Id + ", Name: " + result.HotelInfo.Name);
                    }

                    
                    if (node.Name == "addr")
                    {
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item.Name == "line1"){
                                result.HotelInfo.Address.Street1 = item.InnerText;
                            }
                            if (item.Name == "line2"){
                                result.HotelInfo.Address.Street2 = item.InnerText;
                            }

                            if (item.Name == "city"){
                                result.HotelInfo.Address.City = new City { Name = item.InnerText };
                            }

                            if (item.Name == "state"){
                                result.HotelInfo.Address.State = new State { Code = item.Attributes.Item(0).InnerText, Name = item.Attributes.Item(1).InnerText };
                            }

                            if (item.Name == "country"){
                                result.HotelInfo.Address.Country = new Country { Code = item.Attributes.Item(0).InnerText, Name = item.Attributes.Item(1).InnerText };
                            }

                            if (item.Name == "postal"){
                                result.HotelInfo.Address.Postcode = item.InnerText;
                            }
                        }
                        //Console.WriteLine("  Address is processed");
                    }

                    if (node.Name == "latitude"){
                        result.HotelInfo.Address.Latitude = float.Parse(node.InnerText);
                    }
                    if (node.Name == "longitude"){
                        result.HotelInfo.Address.Longtitude = float.Parse(node.InnerText);
                    }

                    if (node.Name == "stars"){
                        result.HotelInfo.StarRating = float.Parse(node.InnerText);
                    }

                    if (node.Name == "airportsDesc"){
                        result.HotelInfo.AreaInfo = "Airport Description: " + node.InnerText;
                    }

                    if (node.Name == "description"){
                        result.HotelInfo.HotelDescription = node.InnerText;
                    }

                    if (node.Name == "checkInTime")
                    {
                        if (result.HotelInfo.PolicyInfo == null) { result.HotelInfo.PolicyInfo = new HotelPolicy(); }
                        result.HotelInfo.PolicyInfo.CheckInTime = node.InnerText;
                    }
                    if (node.Name == "checkOutTime"){
                        if (result.HotelInfo.PolicyInfo == null) { result.HotelInfo.PolicyInfo = new HotelPolicy(); }
                        result.HotelInfo.PolicyInfo.CheckOutTime = node.InnerText;
                    }

                    if (node.Name == "amenities")
                    {
                        result.HotelInfo.Amenities = new List<Amenity>();
                        foreach (XmlNode amenityNode in node.ChildNodes)
                        {
                            if (amenityNode.FirstChild.Name == "code")
                            {
                                Amenity amenity = new Amenity();
                                amenity.Code = amenityNode.FirstChild.InnerText;
                                if (amenitiesList.getAmenitiesCodeList.Contains(amenity.Code))
                                {
                                    amenity.Name = amenitiesList.getAmenitiesCodeList[amenity.Code].ToString();
                                    result.HotelInfo.Amenities.Add(amenity);
                                }
                                else
                                {
                                    //Console.WriteLine("  *** Amenity code NOT found: '" + amenity.Code + "'   ***");
                                }
                            }

                        }

                        //Console.WriteLine("  Amenities are processed");
                    }


                    if (node.Name == "medias")
                    {
                        result.HotelInfo.Images = new List<HotelImage>();
                        foreach (XmlNode mediaNode in node.ChildNodes)
                        {
                            HotelImage image = new HotelImage();
                            for (int i = 0; i <= mediaNode.ChildNodes.Count - 1; i++)
                            {
                                if (mediaNode.ChildNodes[i].Name == "type"){
                                    image.Code = mediaNode.ChildNodes[i].InnerText;
                                }

                                if (mediaNode.ChildNodes[i].Name == "url"){
                                    image.URL = mediaNode.ChildNodes[i].InnerText;
                                }
                            }
                            result.HotelInfo.Images.Add(image);
                        }

                        //Console.WriteLine("  Medias are processed");
                    }

                    if (node.Name == "facilities")
                    {
                        result.HotelInfo.Facilities = new List<Facility>();
                        foreach (XmlNode facilityNode in node.ChildNodes)
                        {
                            Facility facility = new Facility();
                            for (int i = 0; i <= facilityNode.ChildNodes.Count - 1; i++)
                            {
                                if (facilityNode.ChildNodes[i].Name == "type")
                                {
                                    facility.Code = facilityNode.ChildNodes[i].InnerText;
                                }

                                if (facilityNode.ChildNodes[i].Name == "desc")
                                {
                                    facility.Description = facilityNode.ChildNodes[i].InnerText;
                                }
                            }
                            result.HotelInfo.Facilities.Add(facility);
                        }

                        //Console.WriteLine("  Facilities are processed");
                    }

                    if (node.Name == "userReviews")
                    {
                        result.HotelInfo.GuestReview = new GuestReview();
                        foreach (XmlNode reviewNode in node.ChildNodes)
                        {
                            if (reviewNode.Name == "userScore")
                            {
                                result.HotelInfo.GuestReview.UserScore = float.Parse(reviewNode.InnerText);
                            }
                            if (reviewNode.Name == "numberOfReviews")
                            {
                                result.HotelInfo.GuestReview.NumberOfReviews = long.Parse(reviewNode.InnerText);
                            }
                            if (reviewNode.Name == "numberOfRecommendations")
                            {
                                result.HotelInfo.GuestReview.NumberOfRecommendations = long.Parse(reviewNode.InnerText);
                            }
                        }

                        //Console.WriteLine("  UserReviews is processed");
                    }

                    if (node.Name == "chainCode"){
                        result.HotelInfo.Chain = new Identification { Code = node.InnerText };
                    }
                     
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }            
        }

        private string getFullPathFileName(long hotelId, string extension)
        {
            return config.OutputJsonFolder + "\\" + config.LanguageCode + "_" + hotelId.ToString() + "." + extension;
        }
    }
}
