using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Rate
    {

        public float  SellRate { get; set; }                //sell rate, this is final price sell to customer
        public float? BaseRate { get; set; }                //full price, price was before saving/discount
        public float? TaxAndServiceFee { get; set; }       //tax rate-service fee, null = unknown tax or n/a
        
        public string CurrencyCode { get; set; }

        public bool? IsPromotionIncluded { get; set; }


    }
}
