using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaWS.Models
{
    [Table("tblADDRESS")]
    public class Address
    {
        public int id { get; set; }
        [DataType(DataType.Text)]
        public string street { get; set; }
        [DataType(DataType.Text)]
        public string city { get; set; }
        [DataType(DataType.Text)]
        public string town { get; set; }
        [DataType(DataType.Text)]
        public string area { get; set; }
        [DataType(DataType.PostalCode)]
        public string code { get; set; }
        public Account account { get; set; }
    }
}