using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "This field can't be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Can't be zero or negative")]
        public int Number { get; set; }
        
        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string District { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(100, ErrorMessage = "This field can't pass 100 chars")]
        public string CEP { get; set; }

        // Chaves estrangeiras
        public string UserId { get; set; }
        public User User { get; set; }
    }
}