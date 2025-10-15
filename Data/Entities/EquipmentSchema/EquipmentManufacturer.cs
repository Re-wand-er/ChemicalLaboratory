using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class EquipmentManufacturer
    {
        public int idEquipmentManufacturer { get; set; }
        public int idEquipment { get; set; }
        public int idManufacturer { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public DateOnly GuaranteeDate { get; set; }

        public Equipment? Equipment {  get; set; }
        public ManufacturerEquip? ManufacturerEquipments { get; set; }
    }
}
