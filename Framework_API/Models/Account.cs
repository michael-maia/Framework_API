using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class Account
    {
        public int Id { get; set; }

        // Chave estrangeira
        public string UserId { get; set; }
        public User User { get; set; }
    }
}