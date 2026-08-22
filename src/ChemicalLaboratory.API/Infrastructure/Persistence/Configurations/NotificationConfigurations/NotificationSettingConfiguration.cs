using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class NotificationSettingConfiguration : IEntityTypeConfiguration<NotificationSetting>
    {
        public void Configure(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder.ToTable("NotificationSettings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LowQuantityThreshold)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.EmailTemplate)
                .HasMaxLength(2048);
        }
    }
}
