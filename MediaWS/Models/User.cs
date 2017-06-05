using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaWS.Models
{
    [Table("tblUSER")]
    public class User
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Name Field can't be empty")]
        [DataType(DataType.Text, ErrorMessage = "Text only")]
        public string name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Surname Field can't be empty")]
        [DataType(DataType.Text, ErrorMessage = "Text only")]
        public string surname { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password Field can't be empty")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        //[StringLength(8, MinimumLength = 15)]
        public string password { get; set; }

        [NotMapped]  //Does not effect with your database
        [Required(ErrorMessage = "Confirm Password Field can't be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [CompareAttribute("password", ErrorMessage = "Password doesn't match")]
        public string confirm { get; set; }

        [Required(ErrorMessage = "SA id number Field can't be empty")]
        [DataType(DataType.Text, ErrorMessage = "Not a valid SA ID")]
        [Display(Name = "SA ID")]
        public string idOfbirth { get; set; }

        public ICollection<Blog> blog { get; set; }
        public ICollection<Account> account { get; set; }
        public ICollection<Verify> verify { get; set; }
    }

}
