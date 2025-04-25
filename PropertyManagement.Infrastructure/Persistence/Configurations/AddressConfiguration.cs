using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Define Table Name and Schema
            builder.ToTable("Address", "dbo");

            // Define Primary Key
            builder.HasKey(p => p.Id);

            // Configure Properties
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Line1).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Line2).IsRequired().HasMaxLength(500);
            builder.Property(p => p.City).IsRequired().HasMaxLength(255);
            builder.Property(p => p.State).IsRequired().HasMaxLength(255);
            builder.Property(p => p.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Country).IsRequired().HasMaxLength(255);       
        }
    }
}
