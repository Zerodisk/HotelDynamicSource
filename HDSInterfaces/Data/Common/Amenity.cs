using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Amenity : Identification 
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
