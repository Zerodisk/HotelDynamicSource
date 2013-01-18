using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HDSInterfaces
{
    public class Session
    {
        //client and customer details
        public string SessionId { get; set; }               //24 digits session id for each customer session. format could be siteid-yyyymmddhhmm-[7digit random]
                                                            // example 101-201301171340-qlkdufp
        public string ClientIpAddress { get; set; }         //server ip who send the request
        public string CustomerIpAddress { get; set; }       //real customer ip
        public string BrowserUserAgent { get; set; }
        
        //multi language-currency
        public string CurrencyCode { get; set; }
        public string LanguageCode { get; set; }
        public string Locale { get; set; }

        //authentication and security validate for client
        public string SiteId { get; set; }                  //client primary key (first client, start with 101, then 102, 103)
        public string UserAccess { get; set; }              //client identification code
        public string EncryptHash { get; set; }                  //hash for security validation
        /*
         * sha1(combination of the following, separated by #)
         * - checkin
         * - checkout
         * - session id 
         * - user access
         * - sha1(AccessCode)
         * 
         * assume sessionId   = 101-201301171340-qlkdufp
         * assume user access = 1234
         * assume access code = abcd
         * 
         * sha1(2013-01-12#2013-01-15#101-201301171340-qlkdufp#1234#sha1(abcd))
         */


        private string AccessCode;  //client password (never sent as clear text)

        public void SetAccessCode(string value)
        {
            AccessCode = value;
        }

        public void SetEncryptHash(DateTime checkIn, DateTime checkOut){

        }
    }
}
