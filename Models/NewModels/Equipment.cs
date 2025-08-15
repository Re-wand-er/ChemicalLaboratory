using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class Equipment
    {
        [Key]
        public int IdEquipment { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Kind { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ExperimentEquipment> ExperimentEquipments { get; set; }
        public virtual ICollection<EquipmentManufacturer> EquipmentManufacturers { get; set; }
    }
}