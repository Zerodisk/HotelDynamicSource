using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class ReservationInformation
    {
        public Customer MainCustomer { get; set; }
        public CreditCard CreditCardDetail { get; set; }
    }
}
