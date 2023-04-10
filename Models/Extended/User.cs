using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Rentisha.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
    public class UserMetaData
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="First Name Required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number Required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Minimum 6 characters required")]
        public string Password { get; set; }

      
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords Do not match")]
        public string ConfirmPassword { get; set; }


    }
}