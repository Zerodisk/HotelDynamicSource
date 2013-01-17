using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Customer : BasePerson 
    {
        public Address HomeAddress { get; set; }
        public Address BillingAddress { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}
