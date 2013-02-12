using System;
using System.Collections.Generic;
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
            string json = "";
            RequestManager rqManager = new RequestManager();

            //generate HDSRequest
            HDSRequest rq = rqManager.GetRequest(Request);

            if (rq.IsRequestError)
            {
                //error in generate HDSRequest object
                json = this.GenerateJson(this.GenerateError((long)rq.Error.Id, rq.Error.Message));
            }
            else
            {
                //switch to get response
                switch (rq.RequestType)
                {
                    case HDSRequestType.SearchByLocationKeyword:
                        json = rqManager.GetSearchResult(rq, Request);
                        break;
                    case HDSRequestType.SearchByLocationIds:
                        json = rqManager.GetSearchResult(rq, Request);
                        break;
                    case HDSRequestType.SearchByHotelIds:
                        json = rqManager.GetSearchResult(rq, Request);
                        break;
                    case HDSRequestType.SearchByHotelId:
                        json = rqManager.GetHotelAvailability(rq);
                        break;
                    case HDSRequestType.HotelContent:
                        json = rqManager.GetHotelInfo(rq);
                        break;
                }
            }

            this.RendeResponse(json);
        }








        /*
         * this is main function for returning response with specific header
         */ 
        private void RendeResponse(string json)
        {
            //set http header (e.g. content type, etc)
            Response.AddHeader("Content-Type", "application/json; charset=utf-8 ");
            Response.Charset         = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(json);
        }

        /*
         * for generate error object for a given error id and message
         */ 
        private BaseResponse GenerateError(long id, string message)
        {
            BaseResponse rs = new BaseResponse();
            rs.Session      = null;
            rs.Errors       = new List<WarningAndError>();
            rs.Errors.Add(new WarningAndError { Id = id, Message = message });
            return rs;
        }

        /*
         * a wrapper function for convert object into json string
         */ 
        private string GenerateJson(object obj)
        {
            OutputFormatter outputManager = new OutputFormatter();
            return outputManager.ObjectToJson(obj);
        }
    }
}
