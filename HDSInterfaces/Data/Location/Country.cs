using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Country : Identification 
    {
        public string CountryPhoneCode { get; set; }
        public List<City> Cities { get; set; }
    }
}
