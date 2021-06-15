using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class User : IdentityUser
    {        
        public string FullName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserEmail { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }

        // Chaves estrangeiras
        public ICollection<Rent> Rents { get; set; }       
        
        public Account Account { get; set; }
    }
}