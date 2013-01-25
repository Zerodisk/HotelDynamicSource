using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class HotelContentRS : BaseResponse 
    {
        public List<Hotel> Hotels { get; set; }
    }
}
