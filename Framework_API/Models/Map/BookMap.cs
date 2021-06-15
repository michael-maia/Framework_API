using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.AuthorFullName).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Edition).IsRequired();
            builder.Property(b => b.Year).IsRequired();
            builder.Property(b => b.NumberOfPages).IsRequired();

            // Definindo relação entre entidades
            // Ao deletar um registro vai deletar todos que estiverem relacionados ao ID do livro (remoção em cascata)
            builder.HasMany(b => b.Rents).WithOne(b => b.Book).OnDelete(DeleteBehavior.Cascade);
        }
    }
}