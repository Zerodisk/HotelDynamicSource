using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class GuestReview
    {
        public float? UserScore {get;set;}
        public long? NumberOfReviews { get; set; }
        public long? NumberOfRecommendations { get; set; }
    }
}
