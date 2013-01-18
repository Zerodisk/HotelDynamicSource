using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Location
    {
        public List<Identification> LocationIds { get; set; }
        public string LocationKeyword { get; set; }
        public decimal MaxDistance { get; set; }
        public Address Address { get; set; }
     
    }
}
