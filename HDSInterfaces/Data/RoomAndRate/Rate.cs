using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Rate
    {

        public decimal SellRate { get; set; }               //sell rate, this is final price sell to customer
        public decimal BaseRate { get; set; }               //full price, price was before saving/discount
        public decimal TaxRate { get; set; }                //tax amount if there is any
        public string  CurrencyCode { get; set; }

        public DateTime? NightStayDate { get; set; }
        public bool? IsIncludePromotion { get; set; }

        public bool IsRateInclusiveOfTax
        {
            get{
                if (TaxRate == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
