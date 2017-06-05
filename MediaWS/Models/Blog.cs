using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaWS.Models
{
    [Table("tblBLOG")]
    public class Blog
    {
        public int id { get; set; }
        public string title { get; set; }

        [Required(ErrorMessage ="Comments posts can not be empty")]
        [DataType(DataType.MultilineText)]
        public string body { get; set; }
        [DataType(DataType.Upload)]
        [MaxLength]
        public byte[] picture { get; set; }
        public User user { get; set; }
        public ICollection<Comment> comment { get; set; }
    }
}