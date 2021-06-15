using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.City).IsRequired();
            builder.Property(u => u.State).IsRequired();
            builder.Property(u => u.BirthDate).IsRequired();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(11);
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.Password).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Gender).IsRequired();

            // Configurando para ser desconsiderado algumas configurações padrão
            builder.Ignore(i => i.EmailConfirmed);
            builder.Ignore(i => i.AccessFailedCount);
            builder.Ignore(i => i.LockoutEnabled);
            builder.Ignore(i => i.LockoutEnd);
            builder.Ignore(i => i.PhoneNumber);
            builder.Ignore(i => i.PhoneNumberConfirmed);
            builder.Ignore(i => i.TwoFactorEnabled);

            // Declarando as relações entre entidades
            builder.HasMany(u => u.Rents).WithOne(u => u.User);
            // Em uma relação 1-N não precisamos identificar a entidade dependente no HasForeignKey (https://docs.microsoft.com/pt-br/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#one-to-one)
            builder.HasOne(u => u.Role).WithMany(u => u.Users).HasForeignKey(u => u.RoleId);
            // Ao definir uma chave estrangeira 1-1 é necessário especificar a entidade dependente no HasForeignKey
            builder.HasOne(u => u.Account).WithOne(u => u.User).HasForeignKey<Account>(u => u.UserId);
        }
    }
}