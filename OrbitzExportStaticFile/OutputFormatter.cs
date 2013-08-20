using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace OrbitzExportStaticFile
{
    public class OutputFormatter
    {
        /*
         * function to convert object to jsaon string
         */ 
        public string ObjectToJson(object obj)
        {
            string output = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return output;
        }

        /*
         * function to write a given text string into a file
         *   text - is input text string (json string in this case)
         *   fullPathFileName - is a full path file name (e.g. c:\temp\en_1234.json)
         */
        public bool WriteToFile(string text, string fullPathFileName)
        {
            try
            {
                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter(fullPathFileName);
                file.WriteLine(text);

                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
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
                    new XmlTextWriter(memoryStream, Encoding.UTF8) { Formatting = System.Xml.Formatting.Indented })
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
