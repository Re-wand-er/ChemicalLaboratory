using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configurations
{
    internal class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.ToTable("WorkSchedule", schema: "PeopleSchema");
            //CONSTRAINT UQ_WorkShift UNIQUE (StartTime, EndTime)
            builder.HasKey(ws => ws.idWorkSchedule);

            builder.Property(ws => ws.WorkShift).HasDefaultStringConfig(15, isRequired: false);
            builder.Property(ws => ws.StartTime).IsRequired();
            builder.Property(ws => ws.EndTime).IsRequired();
        }
    }
}
