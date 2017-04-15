using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dream.Models
{
    public class PostContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PostContext() : base("name=PostContext")
        {
        }

        public System.Data.Entity.DbSet<Dream.Models.Post> Posts { get; set; }
        public System.Data.Entity.DbSet<Dream.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<Dream.Models.Rating> Ratings { get; set; }
    }
}
