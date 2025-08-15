using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class EquipmentManufacturerCompany
    {
        [Key]
        public int IdManufacturer { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<EquipmentManufacturer> Equipments { get; set; }
    }
}