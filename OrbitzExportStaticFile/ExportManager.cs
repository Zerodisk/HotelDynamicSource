using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using HDSInterfaces;

namespace OrbitzExportStaticFile
{
    public class ExportManager
    {
        private OutputFormatter outputFormat = new OutputFormatter();
        private AmenityCode amenitiesList = new AmenityCode();

        //main method to export
        public void DoStart()
        {
            string xmlFile = this.InputXmlFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);


        }

        public string InputXmlFile
        {
            get { return ConfigurationManager.AppSettings["inputXmlFile"]; }
        }

        public string OutputJsonFolder
        {
            get { return ConfigurationManager.AppSettings["outputFolder"];  }
        }

        public string LanguageCode
        {
            get { return ConfigurationManager.AppSettings["languageCode"]; }
        }

        public string getFullPathFileName(long hotelId)
        {
            return this.OutputJsonFolder + "\\" + this.LanguageCode + "_" + hotelId.ToString() + ".json";
        }
    }
}
