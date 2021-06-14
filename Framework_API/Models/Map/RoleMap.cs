using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        // Mapeamento da classe Role
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.Property(u => u.Name).IsRequired().HasMaxLength(20);
        }
    }
}