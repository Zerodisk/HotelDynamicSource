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
         * 90xx is provider problem 
         * --------------------------------------------------------
         * 9001 = error return from expedia
         * 9002 = waiting return from expedia
         * 9003 = error from provider (e.g. server too busy)
         * 
         * 91xx mapping from provider object to HDSObject exception
         * -------------------------------------------------------
         * 9150 = hotel content mapping exception
         * 9140 = itinerary/cancellation mapping exception
         * 9130 = hotel reprice/raterule and booking mapping exception
         * 9120 = hotel availability mapping exception
         * 9110 = serach result mapping exception
         */




    }

}
