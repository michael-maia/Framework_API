using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Chave estrangeira
        public ICollection<User> Users { get; set; }
    }
}