using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class PostView
    {
        public Post post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}