using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    /*
     *  all date format string is always yyyy-MM-dd   1974-07-15
     *    although expedia return in format of MM/dd/yyyy
     */ 
    public class StayDate
    {
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }

        public DateTime? DateCheckIn
        {
            get{
                if (CheckIn == null){return null;}

                try{
                    return DateTime.ParseExact(CheckIn, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e){
                    return null;
                }
            }
        }

        public DateTime? DateCheckOut
        {
            get{
                if (CheckOut == null){ return null; }

                try{
                    return DateTime.ParseExact(CheckOut, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e){
                    return null;
                }
            }
        }

    }
}
