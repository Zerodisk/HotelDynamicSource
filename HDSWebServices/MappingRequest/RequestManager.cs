using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HDSInterfaces;
using HotelDynamicSource;

namespace HDSWebServices
{
    public class RequestManager
    {
        OutputFormatter outputManager;

        public RequestManager()
        {
            outputManager = new OutputFormatter();
        }
        
        /*
         * mapping REST request to HDSRequest object !!!!
         */ 
        public HDSRequest GetRequest(HttpRequest httpRq)
        {
            /*
             * 
             * validation request 
             * 
             */

            //get type of request (e.g search result, hotel avail)
            int cmdType = int.Parse(httpRq["cmdType"]);         
            HDSRequest request = new HDSRequest(MapRequestType(cmdType));

            //get select provider source (e.g. expedia, orbitz)
            string source = httpRq["source"];                  
            if (!string.IsNullOrEmpty(source)){
                request.Session.SourceProvider = this.MapProviderSource(source);
            }

            //process request by request type
            switch (request.RequestType)
            {
                case HDSRequestType.SearchByLocationKeyword:
                    


                    break;
                case HDSRequestType.SearchByHotelId:



                    break;
                case HDSRequestType.HotelContent:
                    HotelContent mapping = new HotelContent();
                    return mapping.MapHotelContent(request, httpRq);
                default:
                    //request type unknown


                    break;
            }

            return request;
        }

        public string GetSearchResult(HDSRequest rq)
        {
            HDSManager sourceManager = this.GetSourceManager();
            SearchResultRS rs = sourceManager.GetSearchResult(rq);
            return outputManager.ObjectToJson(rs);
        }

        public string GetHotelAvailability(HDSRequest rq)
        {
            HDSManager sourceManager = this.GetSourceManager();
            HotelAvailabilityRS rs = sourceManager.GetHotelAvailability(rq);
            return outputManager.ObjectToJson(rs);
        }

        public string GetHotelInfo(HDSRequest rq)
        {
            HDSManager sourceManager = this.GetSourceManager();
            HotelContentRS rs = sourceManager.GetHotelInfo(rq);
            return outputManager.ObjectToJson(rs);
        }







        /*
         * return main hotel dynamic source object
         */ 
        private HDSManager GetSourceManager()
        {
            HDSManager manager = new HDSManager();
            return manager;
        }
        
        /*
         * mapping from source ID to HDSSource
         *  0 = no type
         *  1 = expedia
         *  2 = orbtiz
         */ 
        private HDSSource MapProviderSource(string source)
        {
            int sourceInt;
            try {
                sourceInt = int.Parse(source);
            }
            catch {
                sourceInt = 1;      //int parse failed, use expedia as default
            }

            foreach (HDSSource s in Enum.GetValues(typeof(HDSSource))){
                if ((int)s == sourceInt){return s;}
            }

            return HDSSource.Expedia;       //default is expedia;
        }

        /*
         * mapping from request type if to HDSRequestType
         * 10 = SearchByLocationKeyword
         * 11 = SearchByLocationIds     
         * 12 = SearchByHotelIds       
         * 
         * etc... and so on and on
         */
        private HDSRequestType MapRequestType(int cmdType)
        {
            foreach (HDSRequestType requestType in Enum.GetValues(typeof(HDSRequestType))){
                if ((int)requestType == cmdType) {return requestType;}
            }

            return HDSRequestType.NoType;   //default is no type and it's exception
        }
    }
}
