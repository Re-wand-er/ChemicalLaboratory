using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturer", schema: "ReagentSchema");

            builder.HasKey(m => m.idManufacturer);

            builder.Property(m => m.Name).HasDefaultStringConfig(255, isRequired: true);
            builder.Property(m => m.Email).HasColumnName("email").HasDefaultStringConfig(255, isRequired: true);
            builder.Property(m => m.Address).HasColumnName("address").HasDefaultStringConfig(255, isRequired: true);
            builder.Property(m => m.City).HasDefaultStringConfig(50, isRequired: true);
            builder.Property(m => m.Country).HasColumnName("country").HasDefaultStringConfig(255, isRequired: true);

            // Связь настроена в SupplierConfiguration
            //builder.HasMany(m => m.Suppliers).WithOne(s => s.Manufacturer).HasForeignKey(s => s.idManufacturer);
        }
    }
}
