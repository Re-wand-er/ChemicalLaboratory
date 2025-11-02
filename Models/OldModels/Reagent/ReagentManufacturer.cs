namespace ChemicalLaboratory.Models.Reagent
{
    public class ReagentManufacturer
    {
        public int idReagManuf { get; set; } = 0;
        public RManufacturerDataModel Manufacturer { get; set; } = new RManufacturerDataModel();
        public ReagentDataModel Reagent { get; set; } = new ReagentDataModel();
        public SupplierDataModel Supplier { get; set; } = new SupplierDataModel();
        //public PurityDataModel idPurity { get; set; } = new PurityDataModel();
        public string? PurityClassification { get; set; } = string.Empty;

        public DateTime DateOfManufacture { get; set; } = DateTime.MinValue;
        public string? series { get; set; } = string.Empty;
        public decimal PurityDegree { get; set; } = 0;//string.Empty;


    }
}
