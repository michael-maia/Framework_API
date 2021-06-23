using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
