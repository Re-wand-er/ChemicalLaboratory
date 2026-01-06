using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChemicalLaboratory.Infrastructure.Persistence.Configurations
{
    public class ReagentReceiptConfiguration : IEntityTypeConfiguration<ReagentReceipt>
    {
        public void Configure(EntityTypeBuilder<ReagentReceipt> builder)
        {
            builder.ToTable("ReagentReceipts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.ReceiptDate)
                .IsRequired();

            builder.Property(x => x.DocumentNumber)
                .HasMaxLength(100);

            builder.HasOne(x => x.Reagent)
                .WithMany(x => x.Receipts)
                .HasForeignKey(x => x.ReagentId);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.ReagentReceipts)
                .HasForeignKey(x => x.SupplierId);
        }
    }
}
