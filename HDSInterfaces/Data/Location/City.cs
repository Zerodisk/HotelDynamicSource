using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class City : Identification 
    {
        public decimal? GMTOffSet { get; set; }
        public Country Country { get; set; }
    }
}
