using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Hotel
    {
        //hotel identifier (use Id here only for request, not in response) Hotel ID in response is in HotelInfo.Id
        public long? Id { get; set; }        

        //hotel information
        public HotelInformation HotelInfo { get; set; }

        //rooms information and rates
        public List<Room> Rooms { get; set; }



        //for xml serialization things
        //[System.Xml.Serialization.XmlIgnore]
        //public bool IdSpecified { get { return this.Id != null; } }

    }
}
