using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class BaseRequest
    {
        //type of request (e.g. serach result, hotel page, reprice, booking, etc)
        public HDSRequestType RequestType { get; set; }

        //http request for HDS query string
        public string RequestQueryString { get; set; }

        //session information of the requester (customer ip address, browser user agent, session id, locale and currency)     
        public Session Session { get; set; }

        //if true mean, client is requested for hotel contents
        public bool IsContentRequested { get; set; }

    }
}
