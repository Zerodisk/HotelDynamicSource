using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class RoomRate
    {
        //total rate of this room
        public Rate TotalRate { get; set; }

        //list of nightly rate for this room (rate for each night)
        public List<Rate> NightlyRate { get; set; }


    }
}
