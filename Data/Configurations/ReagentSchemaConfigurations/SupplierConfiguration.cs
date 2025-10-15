using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configurations
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder) 
        {
            builder.ToTable("Supplier", schema: "ReagentSchema");

            builder.HasKey(s => s.idSupplier);

            builder.Property(s => s.Email).HasColumnName("email").HasDefaultStringConfigForNull(255);
            builder.Property(s => s.Name).HasDefaultStringConfigForNull(255);
            builder.Property(s => s.PhoneNumber).HasDefaultStringConfigForNull(20);
            //CONSTRAINT chk_Email CHECK (email LIKE '%@%' AND email LIKE '%.%')

            builder.HasOne(s => s.Manufacturer).WithMany(m => m.Suppliers).HasForeignKey(s => s.idManufacturer);
        }
    }
}
