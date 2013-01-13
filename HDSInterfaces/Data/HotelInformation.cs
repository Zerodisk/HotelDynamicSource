using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HotelInformation
    {
        //general things
        public int? NumberOfRoom { get; set; }
        public int? NumberOfFloor { get; set; }
        public string HotelDescription { get; set; }

        //information
        public string AreaInfo { get; set; }
        public string PropertyInfo { get; set; }
        public string RoomInfo { get; set; }
        public string GenerationInfo { get; set; }
        public string AdditionalInfo { get; set; }

        //location
        public string DrivingDirection { get; set; }
    }
}
