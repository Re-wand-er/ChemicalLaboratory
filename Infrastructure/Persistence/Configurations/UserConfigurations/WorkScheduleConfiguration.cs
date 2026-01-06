using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.ToTable("WorkSchedule");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WorkShift)
                .HasMaxLength(15);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.HasIndex(x => new { x.StartTime, x.EndTime })
                .IsUnique();
        }
    }
}
