using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string AuthorId { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}