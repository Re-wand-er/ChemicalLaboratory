using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Pkix;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class ReagentConfiguration : IEntityTypeConfiguration<Reagent>
    {
        public void Configure(EntityTypeBuilder<Reagent> builder)
        {
            builder.ToTable("Reagents");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.IsActive);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ChemicalFormula)
                .HasMaxLength(100);

            builder.Property(x => x.Unit)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CurrentQuantity)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.MinQuantity)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.StorageLocation)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIME()");

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.DeletedAt);
        }
    }

}
