using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace OrbitzExportStaticFile
{
    public class Config
    {
        public string InputXmlFile
        {
            get { return ConfigurationManager.AppSettings["inputXmlFile"]; }
        }

        public string OutputFolder
        {
            get { return ConfigurationManager.AppSettings["outputFolder"]; }
        }

        public string LanguageCode
        {
            get { return ConfigurationManager.AppSettings["languageCode"]; }
        }
    }
}
