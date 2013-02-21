/*
 * REST request parameter with query string (this is case sensitive querystring name)
 * 
 ** - siteId             = unique ID for client/website on HDS system
 ** - crc                = the encrypted hash to validate the request
 * 
 ** - cmdType            = type of request (e.g. 10 = SearchByLocationKeyword, 20 = SearchByHotelId)
 ** - source             = provide source (e.g. 1 = expedia (default) and 2 = orbitz)
 ** - ipAddress          = customer ip address
 ** - userAgent          = customer browser user agent
 ** - sessionId          = customer session id
 * 
 ** - currCode           = currency code (e.g. AUD, USD)
 ** - locale             = locale (e.g. en_AU, th_TH)
 * 
 ** - checkIn            = check in date in format of yyyy-MM-dd
 ** - checkOut           = check out date in format of yyyy-MM-dd
 * 
 ** - pageIndex          = page index of search result (start with 1)
 ** - pageSize           = page size of search result (max hotel result display per page)
 ** - locationKeyword    = location keyword
 ** - locationId         = location id
 ** - hotelNameKeyword   = additional filter by hotel name keyword
 ** - amenities          = list of amenities separate by comma
 ** - minStar            = minimum hotel star rating
 ** - maxStar            = maximum hotel star rating
 * 
 ** - contentRequested   = request for content, true/false
 ** - hotelId            = a single hotelID
 ** - hotelIds           = multiple hotelID separate by comma (may use in seach by hotel name and get list of hotel Id)
 * 
 ** - numRoom            = number of room
 ** - numAdult1, numAdult2, ..... numAdultN  = number of adult for muliple room
 ** - numChild1, numChild2, ..... numChildN  = string pattern n_x_y_z, n number of children, x,y and z is age of each child (e.g. 1 child age 5 = 1_5), (e.g. 2 children with age 2 and 6 = 2_2_6)
 * - roomCode1, roomCode2, ..... roomCodeN  = room unique identification code
 * 
 ** - cacheKey           = expedia specific for pagination
 ** - cacheLocation      = expedia specific for pagination
 */

using System;
using System.Collections.Generic;
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
            HDSRequest request;
            SearchResult mappingSearch;
            HotelAvailability mappingHotelAvail;
            HotelContent mappingHotelContent;

            try
            {
                //get type of request (e.g search result, hotel avail)
                if (string.IsNullOrEmpty(httpRq["cmdType"])){
                    request = new HDSRequest(HDSRequestType.NoType);
                    request.Error = new WarningAndError { Id = (long)ErrorCode.RequestTypeNotFound, Message = "No Request/Commande type specificed" };
                    return request;
                }

                int cmdType = int.Parse(httpRq["cmdType"]);
                request = new HDSRequest(MapRequestType(cmdType));
                if (request.RequestType == HDSRequestType.NoType) {
                    request.Error = new WarningAndError { Id = (long)ErrorCode.RequestTypeNotMatched, Message = "Request/Command type is not matched" };
                    return request;
                }

                //get select provider source (e.g. expedia, orbitz)
                string source = httpRq["source"];
                if (!string.IsNullOrEmpty(source))
                {
                    request.Session.SourceProvider = this.MapProviderSource(source);
                }

                //request query string for pagination
                request.RequestQueryString        = httpRq.QueryString.ToString();

                //get customer details
                request.Session.CustomerIpAddress = httpRq["ipAddress"];
                request.Session.BrowserUserAgent  = httpRq["userAgent"];
                request.Session.SessionId         = httpRq["sessionId"];

                //get client details
                request.Session.ClientIpAddress   = httpRq.UserHostAddress;
                request.Session.SiteId            = httpRq["siteId"];
                request.Session.EncryptHash       = httpRq["crc"];

                //currency and locale
                request.Session.CurrencyCode      = httpRq["currCode"];
                request.Session.Locale            = httpRq["locale"];
                //validate currency only non-hotelcontent request
                if ((string.IsNullOrEmpty(request.Session.CurrencyCode)) && (request.RequestType != HDSRequestType.HotelContent))
                {
                    request.Error = new WarningAndError { Id = (long)ErrorCode.RequestCurrencyCodeMissing, Message = "CurrencyCode is missing" };
                    return request;
                }

                //process request by request type
                switch (request.RequestType)
                {
                    //serach result -----------------------------------
                    case HDSRequestType.SearchByLocationKeyword:
                        mappingSearch = new SearchResult();
                        request.SearchCriteria = new SearchCriteria();
                        request = mappingSearch.MapSearchByLocationKeyword(request, httpRq);
                        break;
                    case HDSRequestType.SearchByLocationIds:
                        mappingSearch = new SearchResult();
                        request.SearchCriteria = new SearchCriteria();
                        request = mappingSearch.MapSearchByLocationId(request, httpRq);
                        break;
                    case HDSRequestType.SearchByHotelIds:
                        mappingSearch = new SearchResult();
                        request.SearchCriteria = new SearchCriteria();
                        request = mappingSearch.MapSearchByHotelIds(request, httpRq);
                        break;

                    //hotel availability ------------------------------
                    case HDSRequestType.SearchByHotelId:
                        mappingHotelAvail = new HotelAvailability();
                        request = mappingHotelAvail.MapSearchByHotelId(request, httpRq);
                        break;

                    //hotel content -----------------------------------
                    case HDSRequestType.HotelContent:
                        mappingHotelContent = new HotelContent();
                        request = mappingHotelContent.MapHotelContent(request, httpRq);
                        break;

                    //request type unknown ----------------------------
                    default:
                        request.Error = new WarningAndError { Id = (long)ErrorCode.RequestTypeNotPrepared, Message = "Request/Command type requested is not prepared" };
                        break;
                }
            }
            catch (Exception e)
            {
                request = new HDSRequest(HDSRequestType.NoType);
                request.Error = new WarningAndError { Id = (long)ErrorCode.RequestTypeError, Message = e.ToString() };
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

            if (rs.Hotel != null)   //make sure hotel get availability, otherwise will not get content
            {
                if (rq.IsContentRequested)
                {
                    HotelContentRS rsContent = sourceManager.GetHotelInfo(rq);
                    rs.Hotel.HotelInfo = rsContent.Hotels[0].HotelInfo;
                }
            }

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
