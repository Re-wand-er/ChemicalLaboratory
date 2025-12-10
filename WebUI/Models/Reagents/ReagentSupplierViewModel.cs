namespace ChemicalLaboratory.WebUI.Models.Reagents
{
    public class ReagentSupplierViewModel
    {
        public int IdReagSupp { get; set; }
        public int? IdSupplier { get; set; }
        public int IdReagent { get; set; }
        public DateTime DateOfDelivary { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}