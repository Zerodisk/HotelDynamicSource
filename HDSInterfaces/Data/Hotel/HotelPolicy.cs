using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HotelPolicy
    {
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string CheckInInstruction { get; set; }

        public string PolicyDescription { get; set; }        
        public string ChildPolicy { get; set; }
    }
}
