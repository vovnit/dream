using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public int avrgRating { get; set; }
        public DateTime Time { get; set; }
    }
}