using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rentisha.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember me")]
        public bool RememberMe { get; set; }

    }
}