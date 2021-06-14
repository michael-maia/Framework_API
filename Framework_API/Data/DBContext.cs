using Framework_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework_API.Data
{
    public class DBContext : IdentityDbContext<User, Role, string>
    {
        // Entidades a serem criadas no DB
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
    }
}
