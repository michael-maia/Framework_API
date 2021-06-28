using Framework_API.Models;
using Framework_API.Models.Map;
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
        public DbSet<User> UsersTable { get; set; }
        public DbSet<Role> RolesTable { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options){}

        // Estamos definindo as classes a serem consideradas para mapeamento
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new BookMap());
            builder.ApplyConfiguration(new RentMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new AddressMap());
        }
    }
}