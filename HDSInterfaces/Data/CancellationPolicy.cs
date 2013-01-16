using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class CancellationPolicy
    {
        public bool?  IsNonRefundable { get; set; }
        public string CancellationPolicyDescription { get; set; }
        public List<CancellationPolicyTier> CancellationPolicyInfoList { get; set; }
    }
}
