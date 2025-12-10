namespace ChemicalLaboratory.Models.ViewModels
{
    public class EquipmentManufacturerViewModel
    {
        public int IdEquipmentManufacturer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime GuaranteeDate { get; set; }

        public virtual EquipmentViewModel? Equipment { get; set; }
        public virtual EManufactureViewModel? Manufacturer { get; set; }
    }
}