using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is Required")]
        [StringLength(100,ErrorMessage ="Username/Email must be between 5 to 100 characters",MinimumLength =5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$",ErrorMessage =("Must be a valid Email"))]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20,ErrorMessage ="Password must be between 8 to 255 characters",MinimumLength =8)]
        public string PasswordHashed { get; set; }
    }
}