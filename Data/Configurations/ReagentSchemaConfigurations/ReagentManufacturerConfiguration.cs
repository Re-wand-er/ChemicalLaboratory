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
    internal class ReagentManufacturerConfiguration :IEntityTypeConfiguration<ReagentManufacturer>
    {
        public void Configure(EntityTypeBuilder<ReagentManufacturer> builder)
        {
            builder.ToTable("ReagentManufacturer", schema: "ReagentSchema");

            builder.HasKey(rm => rm.IdReagManuf);

            builder.Property(rm => rm.Series).HasColumnName("series").HasDefaultStringConfig(50, isRequired: true);

            builder.HasOne(rm => rm.Reagent)
                .WithMany(r => r.ReagentManufacturers)
                .HasForeignKey(rm => rm.idReagent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rm => rm.Manufacturer)
                .WithMany(m => m.ReagentManufacturers)
                .HasForeignKey(rm => rm.idManufacturer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rm => rm.Purity)
                .WithMany(p => p.ReagentManufacturer)
                .HasForeignKey(rm => rm.idPurity);
        }
    }
}
