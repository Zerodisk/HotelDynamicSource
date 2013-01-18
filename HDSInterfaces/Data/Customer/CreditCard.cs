using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public HDSCreditCardType CardType { get; set; }         //support only visa, mastercard and amex for start
        public string CardExpiryMonth { get; set; }             //2 digits month
        public string CardExpiryYear { get; set; }              //2 digits year
        public string CVV { get; set; }                         //3 of 4 digits

        public Address CardHolderAddress { get; set; }
    }
}
