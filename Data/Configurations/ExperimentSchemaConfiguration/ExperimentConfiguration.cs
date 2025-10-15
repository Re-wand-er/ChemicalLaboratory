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
    internal class ExperimentConfiguration : IEntityTypeConfiguration<Experiment>
    {
        public void Configure(EntityTypeBuilder<Experiment> builder) 
        {
            builder.HasKey(e => e.idExperiment);

            builder.ToTable("Experiment", schema: "ExperimentSchema");

            builder.Property(e => e.Name).HasDefaultStringConfig(255, isRequired: true);
            builder.Property(e => e.Description).HasDefaultStringConfigForNull(500);
            builder.Property(e => e.Result).HasDefaultStringConfigForNull(500);
            builder.Property(e => e.Status).HasColumnName("status").HasDefaultStringConfigForNull(30);
        }
    }
}
