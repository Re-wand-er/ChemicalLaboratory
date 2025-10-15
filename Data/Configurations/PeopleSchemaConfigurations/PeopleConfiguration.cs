using EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Configurations
{
    internal class PeopleConfiguration : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder) 
        {   // index? Интересно как сработает HasCheckConstraint
            builder.ToTable("People", schema: "PeopleSchema").ToTable(p => p.HasCheckConstraint("CK_Sex", "OperationType in ('Insert','Update','Delete')"));

            builder.HasKey(p => p.idPeople);

            builder.Property(p => p.FirstName).HasDefaultStringConfig(20, isRequired: true);
            builder.Property(p => p.MiddleName).HasDefaultStringConfig(30, isRequired: true);
            builder.Property(p => p.LastName).HasDefaultStringConfig(50, isRequired: true);
            builder.Property(p => p.Email).HasColumnName("email").HasDefaultStringConfig(20, isRequired: true);
            
            // CHECK (Sex IN ('М', 'Ж'))
            builder.Property(p => p.Sex).HasDefaultStringConfig(2, isRequired: false);
            builder.Property(p => p.SystemRole).HasDefaultStringConfigForNull(30);
            builder.Property(p => p.JobPosition).HasDefaultStringConfigForNull(30);
            builder.Property(p => p.Login).HasDefaultStringConfig(255, isRequired: true);
            builder.Property(p => p.PasswordHash).HasDefaultStringConfigForNull(255);

            builder.HasOne(p => p.WorkSchedule)
                .WithMany(ws => ws.Peoples)
                .HasForeignKey(p => p.idWorkSchedule);

            // Связь многи ко многим в People -> Experiment
            //builder.HasMany(p => p.Experiment)
            //    .WithMany(e => e.People)
            //    .HasForeignKey(p => p.idExperiment);
        }
    }
}
