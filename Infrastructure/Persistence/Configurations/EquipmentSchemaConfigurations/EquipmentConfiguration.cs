using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(e => e.idEquipment);
            builder.ToTable("Equipment", schema: "EquipmentSchema");

            builder.Property(e => e.Name).HasDefaultStringConfig(125, isRequired: true);
            builder.Property(e => e.Model).HasDefaultStringConfig(100, isRequired: true);
            builder.Property(e => e.Description).HasDefaultStringConfigForNull(255);
            builder.Property(e => e.Kind).HasColumnName("kind").HasDefaultStringConfig(20, isRequired: true);

            //check(Status in ('Используется', 'В ремонте', 'Неисправно', 'Свободно', 'Калибруется')),
            builder.Property(e => e.Status).HasDefaultStringConfigForNull(20);
        }
    }
}
