using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaWS.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string comment { get; set; }
        public User user { get; set; }
        public Blog blog { get; set; }
    }
}