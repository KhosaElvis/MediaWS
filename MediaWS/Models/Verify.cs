using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MediaWS.Models
{
    [Table("tblVERIFY")]
    public class Verify
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "is verified")]
        [DataType(DataType.Text)]
        public bool isVerified { get; set; }
        public int? user_id { get; set; }
        [ForeignKey("user_id")]
        public virtual User user { get; set; }

    }
}