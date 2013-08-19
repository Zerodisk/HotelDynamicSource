using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Hotel
    {
        public long? Id { get; set; }

        //hotel information
        public HotelInformation HotelInfo { get; set; }

        //rooms information and rates
        public List<Room> Rooms { get; set; }

        /*
        public long? Id
        {
            get
            {
                if (HotelInfo == null)
                    return null;
                else
                    return HotelInfo.Id;
            }

            set
            {
                if (HotelInfo == null)
                    HotelInfo = new HotelInformation();

                HotelInfo.Id = value;
            }
        }
        */
    }
}
