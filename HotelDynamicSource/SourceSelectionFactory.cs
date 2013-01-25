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
        public IHDSHotelContent CreateSourceContent(HDSSource sourceCode)
        {
            IHDSHotelContent source = null;

            switch (sourceCode)
            {
                case HDSSource.Expedia:
                    source = new ExpediaManager();
                    break;
                case HDSSource.Orbitz:
                    source = new OrbitzManager();
                    break;
                case HDSSource.Local:                   // local source is internal source where we store information locally in database (e.g. hotel contents)
                    source = null;          
                    break;
            }

            return source;
        }

        public IHDSHotelShopping CreateSourceShopping(HDSSource sourceCode)
        {
            IHDSHotelShopping source = null;

            switch (sourceCode)
            {
                case HDSSource.Expedia: 
                    source = new ExpediaManager();
                    break;
                case HDSSource.Orbitz: 
                    source = new OrbitzManager();
                    break;
            }

            return source;
        }

        public IHDSHotelBooking CreateSourceBooking(HDSSource sourceCode)
        {
            IHDSHotelBooking source = null;

            switch (sourceCode)
            {
                case HDSSource.Expedia:
                    source = new ExpediaManager();
                    break;
                case HDSSource.Orbitz:
                    source = new OrbitzManager();
                    break;
            }

            return source;
        }
    }
}
