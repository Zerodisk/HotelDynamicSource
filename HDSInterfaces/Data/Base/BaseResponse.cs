using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class BaseResponse
    {
        //session information of the requester (customer ip address, browser user agent, session id, locale and currency)     
        public Session Session { get; set; }

        public BaseResponse()
        {
            Session = new Session();
        }
    }
}
