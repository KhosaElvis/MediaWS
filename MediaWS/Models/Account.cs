using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaWS.Models
{
    [Table("tblACCOUNT")]
    public class Account
    {
        public int id { get; set; }
  
        public string birthid { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }
        public User user { get; set; }
        public ICollection<Address> Address { get; set; }

    }
}