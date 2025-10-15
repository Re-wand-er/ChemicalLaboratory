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
    internal class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder) 
        {
            builder.HasKey(h => h.idHistory);

            builder.ToTable("ExperimentHistory", schema: "ExperimentSchema");

            builder.Property(h => h.UserName).HasDefaultStringConfig(255, isRequired: true);
            builder.Property(h => h.OperationType).HasDefaultStringConfigForNull(10);

            // посмотрим как будет работать
            builder.HasOne(h => h.Experiment).WithMany(e => e.ExperimentHistories).HasForeignKey(h => h.idExperiment);
        }
    }
}
