using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ExperimentEquipmentConfigurations : IEntityTypeConfiguration<ExperimentEquipment>
    {
        public void Configure(EntityTypeBuilder<ExperimentEquipment> builder)
        {
            builder.HasKey(ee => ee.idExpEq);
            builder.ToTable("ExperimentEquipment", schema: "dbo");

            builder.HasOne(ee => ee.Experiment).WithMany(e => e.ExperimentEquipments).HasForeignKey(ee => ee.idExperiment);
            builder.HasOne(ee => ee.Equipment).WithMany(e => e.ExperimentEquipments).HasForeignKey(re => re.idEquipment);
        }
    }
}
