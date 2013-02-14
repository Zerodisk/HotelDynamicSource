﻿using System;
using System.Collections.Generic;
using System.Web;

using HDSInterfaces;

namespace HDSWebServices
{
    public class SearchResult
    {
        RequestHelper helper = new RequestHelper();

        //search by location keyword
        public HDSRequest MapSearchByLocationKeyword(HDSRequest request, HttpRequest httpRq)
        {
            request = this.MapGeneral(request, httpRq);

            request.SearchCriteria = new SearchCriteria();
            request.SearchCriteria.LocationKeyword = httpRq["locationKeyword"];

            return request;
        }

        //search by location id
        public HDSRequest MapSearchByLocationId(HDSRequest request, HttpRequest httpRq)
        {
            request = this.MapGeneral(request, httpRq);

            request.SearchCriteria = new SearchCriteria();
            request.SearchCriteria.Location = new Location { Code = httpRq["locationId"] };

            return request;
        }

        //search by list of hotel id
        public HDSRequest MapSearchByHotelIds(HDSRequest request, HttpRequest httpRq)
        {
            request = this.MapGeneral(request, httpRq);

            request.Hotels = new List<Hotel>();
            foreach(string hotelId in httpRq["hotelIds"].Split(',')){
                request.Hotels.Add(new Hotel { Id = long.Parse(hotelId) });
            }

            return request;
        }




        /*
         * general and common value
         *  - checkIn and checkOut
         *  - pageIndex and pageSize
         *  - minStar and maxStar
         */ 
        private HDSRequest MapGeneral(HDSRequest request, HttpRequest httpRq)
        {
            //checkin-out
            request.StayDate = new StayDate { CheckIn = httpRq["checkIn"], CheckOut = httpRq["checkOut"] };

            //page index and page size
            if (!string.IsNullOrEmpty(httpRq["pageIndex"]))
            {
                request.Session.PageIndex = int.Parse(httpRq["pageIndex"]);
            }
            if (!string.IsNullOrEmpty(httpRq["pageSize"]))
            {
                request.Session.PageSize = int.Parse(httpRq["pageSize"]);
            }

            //star rating
            if (!string.IsNullOrEmpty(httpRq["minStar"]))
            {
                request.SearchCriteria.MinStarRating = float.Parse(httpRq["minStar"]);
            }
            if (!string.IsNullOrEmpty(httpRq["maxStar"]))
            {
                request.SearchCriteria.MaxStarRating = float.Parse(httpRq["maxStar"]);
            }
           
            //room(s) request
            request.Itineraries = helper.GenerateItineraryList(httpRq);

            //expedia specific
            if ((httpRq["cacheKey"] != null) && (httpRq["cacheLocation"] != null))
            {
                request.Session.Expedia = new ExpediaSpecific { CacheKey = httpRq["cacheKey"], CacheLocation = httpRq["cacheLocation"] };
            }

            //orbitz specific 
            //  ???

            return request;
        }
    }
}
