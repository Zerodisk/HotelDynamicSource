using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace HDSWebServices
{
    public class OutputFormatter
    {
        public string ObjectToJson(object obj)
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
