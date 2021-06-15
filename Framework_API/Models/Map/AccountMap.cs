using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.HasKey(c => c.AccountId);

            // Definindo relação entre entidades
            builder.HasOne(c => c.User).WithOne(c => c.Account).HasForeignKey<Account>(c => c.UserId);            
        }        
    }
}