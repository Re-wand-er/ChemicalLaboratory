using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class ReagentOperationConfiguration : IEntityTypeConfiguration<ReagentOperation>
    {
        public void Configure(EntityTypeBuilder<ReagentOperation> builder)
        {
            builder.ToTable("reagent_operations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.OperationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.Comment)
                .HasMaxLength(2048);

            builder.HasOne(x => x.Reagent)
                .WithMany(x => x.Operations)
                .HasForeignKey(x => x.ReagentId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ReagentOperations)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.OperationType)
                .WithMany(x => x.ReagentOperations)
                .HasForeignKey(x => x.OperationTypeId);
        }
    }
}
