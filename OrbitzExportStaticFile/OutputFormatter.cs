using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OrbitzExportStaticFile
{
    public class OutputFormatter
    {
        public string ObjectToJson(object obj)
        {
            string output = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return output;
        }
    }
}
