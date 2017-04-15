using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
    }
}