using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Guest : BasePerson 
    {
        public HDSGuestType GuestType { get; set; }
        public Address Address { get; set; }

        public Guest()
        {
            GuestType = HDSGuestType.Adult;
        }
    }
}
