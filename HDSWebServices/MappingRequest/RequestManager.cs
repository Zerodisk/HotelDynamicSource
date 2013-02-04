/*
 * REST request parameter with query string (this is case sensitive querystring name)
 * 
 * - cmdType  = type of request (e.g. 10 = SearchByLocationKeyword, 20 = SearchByHotelId)
 * - source   = provide source (e.g. 1 = expedia (default) and 2 = orbitz)
 * - clientId = unique ID for client on HDS system
 *
 * - checkIn
 * - checkOut
 * - currCode
 * - locale
 * - pageIndex
 * - pageSize
 * 
 * - ipAddress          = customer ip address
 * - userAgent          = customer browser user agent
 * - sessionId          = customer session id
 * 
 * - locationKeyword    = location keyword
 * - hotelId            = a single hotelID
 * - hotelIds           = multiple hotelID separate by comma (may use in seach by hotel name and get list of hotel Id)
 * 
 * - numRoom            = number of room
 * ********** if numRoom = 1 ****************
 * - numAdult                               = number of adult
 * - numChild                               = string pattern n_x_y_z, n number of children, x,y and z is age of each child (e.g. 1 child age 5 = 1_5), (e.g. 2 children with age 2 and 6 = 2_2_6)
 * - roomCode                               = room unique identification code
 * ********** if numRoom > 2 ****************
 * - numAdult1, numAdult2, ..... numAdultN  = number of adult for muliple room
 * - numChild1, numChild2, ..... numChildN  = number of chidren for multiple room
 * - roomCode1, roomCode2, ..... roomCodeN  = room unique identification code
 * 
 */

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
