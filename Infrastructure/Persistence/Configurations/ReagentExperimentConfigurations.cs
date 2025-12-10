using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ReagentExperimentConfigurations : IEntityTypeConfiguration<ReagentExperiment>
    {
        public void Configure(EntityTypeBuilder<ReagentExperiment> builder)
        {
            builder.HasKey(re => re.idReagExpetiment);
            builder.ToTable("ReagentExperiment", schema: "dbo");

            builder.HasOne(re => re.Reagent).WithMany(r => r.ReagentExperiments).HasForeignKey(re => re.idReagent);
            builder.HasOne(re => re.Experiment).WithMany(e => e.ReagentExperiments).HasForeignKey(re => re.idExperiment);
        }
    }
}
