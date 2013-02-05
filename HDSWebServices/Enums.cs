using System;
using System.Collections.Generic;
using System.Web;

namespace HDSWebServices
{

    /*
     * list of all error code return by HDSWebServices
     *  basically this is a place to register known error
     */ 
    public enum ErrorCode{
        RequestTypeNotFound         = 9999,
        RequestTypeNotMatched       = 9998,
        RequestTypeNotPrepared      = 9997,
        RequestCurrencyCodeMissing  = 9996,
        RequestTypeError            = 9990


        /*
         * 9001 = error return from expedia
         * 9002 = waiting return from expedia
         */ 
    }

}
