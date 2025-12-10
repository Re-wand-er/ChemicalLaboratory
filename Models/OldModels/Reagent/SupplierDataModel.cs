namespace ChemicalLaboratory.Models.Reagent
{
    public class SupplierDataModel
    {
        public int idSupplier { get; set; } = 0;
        public RManufacturerDataModel idManufacturer { get; set; } = new RManufacturerDataModel();
        public string? email { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public SupplierDataModel() { }
    }
}
