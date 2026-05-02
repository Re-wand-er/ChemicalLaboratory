using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class ReagentCategoryConfiguration : IEntityTypeConfiguration<ReagentCategory>
    {
        public void Configure(EntityTypeBuilder<ReagentCategory> builder)
        {
            builder.ToTable("ReagentCategories");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.IsActive);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(2048);

            builder.HasMany(x => x.Reagents)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.DeletedAt);
        }
    }
}
