﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class Role : IdentityRole
    {
        public string Name { get; set; }
        public ICollection<User> User { get; set; }
    }
}