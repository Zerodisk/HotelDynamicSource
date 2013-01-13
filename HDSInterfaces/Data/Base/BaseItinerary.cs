using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class BaseItinerary : Identification
    {
        public HDSItineraryType ItineraryType { get; set; }

        public BaseItinerary()
        {
            //auto set default to room (expedia and orbitz are support only room at the moment)
            ItineraryType = HDSItineraryType.Room;
        }
    }
}
