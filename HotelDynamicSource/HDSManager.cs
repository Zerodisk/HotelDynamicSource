using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDSInterfaces;

namespace HotelDynamicSource
{
    /*
     *  this is a main class to content, shopping and booking
     */ 
    public class HDSManager
    {
        public HotelContentRS GetHotelInfo(HDSRequest rq)
        {
            SourceSelectionFactory factory = new SourceSelectionFactory();
            IHDSHotelContent contentObj = factory.CreateSourceContent(rq.Session.SourceProvider);
            return contentObj.GetHotelInfo(rq);
        }

        public SearchResultRS GetSearchResult(HDSRequest rq)
        {
            SourceSelectionFactory factory = new SourceSelectionFactory();
            IHDSHotelShopping shoppingObj = factory.CreateSourceShopping(rq.Session.SourceProvider);  
            return shoppingObj.GetSearchResult(rq);
        }

        public HotelAvailabilityRS GetHotelAvailability(HDSRequest rq)
        {
            SourceSelectionFactory factory = new SourceSelectionFactory();
            IHDSHotelShopping shoppingObj = factory.CreateSourceShopping(rq.Session.SourceProvider);
            return shoppingObj.GetHotelAvailability(rq);
        }

    }
}
