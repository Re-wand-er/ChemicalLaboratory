using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Sex)
                .HasMaxLength(2);

            builder.Property(x => x.SystemRole)
                .HasMaxLength(30);

            builder.Property(x => x.JobPosition)
                .HasMaxLength(30);

            builder.Property(x => x.Login)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.WorkSchedule)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdWorkSchedule);
        }
    }
}
