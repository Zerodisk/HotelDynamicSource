using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class WarningAndError : Identification 
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string DetailDescription { get; set; }
    }
}
