using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Field Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Field Required")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Field Required")]
        [StringLength(14,ErrorMessage ="Invalid Phone Number",MinimumLength =10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = ("Invalid Phone Number"))]
        public string PhoneNumber { get; set; }
        
        public string City { get; set; }
        
        public string Province { get; set; }

        [Display(Name = "postal Code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Postal Code Required")]
        [StringLength(6,ErrorMessage ="Must be 6 Characters",MinimumLength =6)]
        public string PostalCode { get; set; }

        //public List<string> CountryLists { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Field Required")]
        public string Country { get; set; }
    }
}