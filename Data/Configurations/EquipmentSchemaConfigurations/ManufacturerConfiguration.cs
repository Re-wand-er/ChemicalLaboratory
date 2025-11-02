using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Configurations
{
    internal class ManufacturerEquipConfiguration : IEntityTypeConfiguration<ManufacturerEquip>
    {
        public void Configure(EntityTypeBuilder<ManufacturerEquip> builder)
        {
            builder.HasKey(me => me.idManufacturer);
            builder.ToTable("Manufacturer", schema: "EquipmentSchema");

            builder.Property(me => me.Email).HasColumnName("email").HasDefaultStringConfig(255, isRequired: true);
            builder.Property(me => me.PhoneNumber).HasDefaultStringConfigForNull(50);
            builder.Property(me => me.Address).HasColumnName("address").HasDefaultStringConfigForNull(255);
            builder.Property(me => me.City).HasDefaultStringConfigForNull(255);
            builder.Property(me => me.Country).HasDefaultStringConfigForNull(255);
        }
    }
}
