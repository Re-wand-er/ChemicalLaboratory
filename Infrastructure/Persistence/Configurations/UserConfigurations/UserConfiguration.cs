using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.IsActive);

            builder.Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Sex)
                .HasMaxLength(2);

            builder.Property(x => x.JobPosition)
                .HasMaxLength(30);

            builder.Property(x => x.Login)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Login)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.DeletedAt);

            builder.HasOne(x => x.WorkSchedule)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdWorkSchedule);

            builder.HasOne(x => x.SystemRole)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.SystemRoleId);
        }
    }
}
