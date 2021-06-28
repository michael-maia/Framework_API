using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.AddressId);

            builder.Property(e => e.Street).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Number).IsRequired();
            builder.Property(e => e.District).IsRequired().HasMaxLength(100);
            builder.Property(e => e.City).IsRequired().HasMaxLength(100);
            builder.Property(e => e.State).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CEP).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.User).WithMany(e => e.Addresses).HasForeignKey(e => e.UserId);            
        }
    }
}
