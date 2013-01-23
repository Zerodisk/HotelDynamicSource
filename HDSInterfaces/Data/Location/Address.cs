using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Address
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }

        public string Phong { get; set; }
        public string Fax { get; set; }

        public City City { get; set; }
        public Country Country { get; set; }

        public float? Latitude { get; set; }
        public float? Longtitude { get; set; }

        public string LocationDescription { get; set; }
    }
}
