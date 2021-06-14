using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class RentMap : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();

            // Definindo relação entre entidades
            builder.HasOne(r => r.User).WithMany(r => r.Rents).HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Book).WithMany(r => r.Rents).HasForeignKey(r => r.BookId);
        }
    }
}