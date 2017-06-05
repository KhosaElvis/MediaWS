using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MediaWS.Models
{
    public class MediaContent : DbContext
    {      
        public DbSet<Account> accounts { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Verify> verifies { get; set; }

    }
}