using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Identification
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        //for xml serialization things
        //[System.Xml.Serialization.XmlIgnore]
        //public bool IdSpecified { get { return this.Id != null; } }
    }
}
