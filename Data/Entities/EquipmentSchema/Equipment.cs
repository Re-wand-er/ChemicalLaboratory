using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class Equipment
    {
        public int idEquipment { get; set; }
		public string Name { get; set; } = null!;
		public string Model { get; set; } = null!;
		public string? Description { get; set; } = null!;
		public string Kind { get; set; } = null!;
		public string? Status { get; set; } = null!; // check(Status in ('Используется', 'В ремонте', 'Неисправно', 'Свободно', 'Калибруется')),
		    
		public ICollection<EquipmentManufacturer> EquipmentManufacturers { get; set; } = new List<EquipmentManufacturer>();
		public ICollection<ExperimentEquipment> ExperimentEquipments { get; set; } = new List<ExperimentEquipment>();
	}
}
