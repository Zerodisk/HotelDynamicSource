using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Promotion : Identification
    {
        public string Description { get; set; }             //short description of promotion
        public string DetailDescription { get; set; }       //long detail promotion
    }
}
