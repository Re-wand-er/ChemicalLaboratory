using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class EquipmentManufacturer
    {
        [Key]
        public int IdEquipmentManufacturer { get; set; }
        public int IdEquipment { get; set; }
        public int IdManufacturer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime GuaranteeDate { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}