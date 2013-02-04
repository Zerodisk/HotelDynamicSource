using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HDSInterfaces;
using HotelDynamicSource;

namespace HDSWebServices
{
    public partial class GetData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //create neccessary objects
            string json;
            RequestManager rqManager = new RequestManager();

            //generate HDSRequest
            HDSRequest rq = rqManager.GetRequest(Request);

            //swithc to get response
            switch (rq.RequestType)
            {
                case HDSRequestType.SearchByLocationKeyword:
                    json = rqManager.GetSearchResult(rq);
                    this.RendeResponse(json);
                    break;
                case HDSRequestType.SearchByHotelId:
                    json = rqManager.GetHotelAvailability(rq);
                    this.RendeResponse(json);
                    break;
                case HDSRequestType.HotelContent:
                    json = rqManager.GetHotelInfo(rq);
                    this.RendeResponse(json);
                    break;
                default:
                    //request type unknown


                    break;
            }
        }

        private void RendeResponse(string json)
        {
            //set http header (e.g. content type, etc)
            Response.AddHeader("Content-Type", "application/json");
            Response.Write(json);
        }
    }
}
