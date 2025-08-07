namespace ChemicalLaboratory.Models.Equipment
{
    public class EquipmentManufacturer
    {
        public int idEquipmentManufacturer { get; set; } = 0;
        public EquipmentDataModel Equipment { get; set; } = new EquipmentDataModel();
        public EManufacturerDataModel Manufacturer { get; set; } = new EManufacturerDataModel();
        public DateOnly PurchaseDate { get; set; }      = DateOnly.MinValue;
        public DateOnly GuaranteeDate { get; set; }     = DateOnly.MinValue;

        public EquipmentManufacturer() { }
    }
}
