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
    internal class EquipmentManufacturerConfiguration : IEntityTypeConfiguration<EquipmentManufacturer>
    {
        public void Configure(EntityTypeBuilder<EquipmentManufacturer> builder) 
        {
            builder.HasKey(em => em.idEquipmentManufacturer);
            builder.ToTable("EquipmentManufacturer", schema: "EquipmentSchema");

            builder.HasOne(em => em.ManufacturerEquipments).WithMany(m => m.EquipmentManufacturers).HasForeignKey(em => em.idManufacturer);
            builder.HasOne(em => em.Equipment).WithMany(e => e.EquipmentManufacturers).HasForeignKey(em => em.idEquipment);
        }
    }
}
