using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(20, ErrorMessage = "This field can't pass 20 chars")]
        public string UserName { get; set; }        

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(20, ErrorMessage = "This field can't pass 20 chars")]
        [DataType(DataType.Password)]
        public string Password { get; set; }      
    }
}