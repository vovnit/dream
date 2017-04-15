using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class FeedView
    {
        public List<Post> Posts { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}