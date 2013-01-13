using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expedia;
using Orbitz;
using HDSInterfaces;

namespace HotelDynamicSource
{
    public class SourceSelectionFactory
    {
        public IHDSHotelShopping CreateSourceShopping(HDSSource sourceCode)
        {
            IHDSHotelShopping source;

            switch (sourceCode)
            {
                case HDSSource.Expedia: 
                    source = new ExpediaManager();
                    break;
                case HDSSource.Orbitz: 
                    source = new OrbitzManager();
                    break;
                default:
                    source = null;
                    break;
            }

            return source;
        }

        public IHDSHotelBooking CreateSourceBooking(HDSSource sourceCode)
        {
            IHDSHotelBooking source;

            switch (sourceCode)
            {
                case HDSSource.Expedia:
                    source = new ExpediaManager();
                    break;
                case HDSSource.Orbitz:
                    source = new OrbitzManager();
                    break;
                default:
                    source = null;
                    break;
            }

            return source;
        }
    }
}
