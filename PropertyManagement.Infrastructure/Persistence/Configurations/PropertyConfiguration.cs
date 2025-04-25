using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // Define Table Name and Schema
            builder.ToTable("Property", "dbo"); 

            // Define Primary Key
            builder.HasKey(p => p.Id);

            // Configure Properties
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.ExternalId).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.AddressId).IsRequired();

            builder.HasOne(p => p.Address)
              .WithOne(a => a.Property)
              .HasForeignKey<Property>(a => a.AddressId);

        }
    }
}
