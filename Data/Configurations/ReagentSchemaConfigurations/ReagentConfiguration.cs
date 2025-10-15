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
    internal class ReagentConfiguration :IEntityTypeConfiguration<Reagent>
    {
        public void Configure(EntityTypeBuilder<Reagent> builder)
        {
            builder.ToTable("Reagent", schema: "ReagentSchema");

            builder.HasKey(r => r.idReagent);

            builder.Property(r => r.Name).HasDefaultStringConfig(50, isRequired: true);

            //    .HasPrecision(7, 3) на случай изменений закомментировал
            builder.Property(r => r.Density).HasColumnName("Dansity").IsRequired();
            builder.Property(r => r.ChemicalFormula).HasDefaultStringConfig(50, isRequired: true);

            //    .HasPrecision(10, 2) на случай изменений закомментировал
            builder.Property(r => r.Mass).HasColumnName("mass").IsRequired();

        }
    }
}
