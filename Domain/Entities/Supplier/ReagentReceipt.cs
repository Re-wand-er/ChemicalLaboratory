namespace ChemicalLaboratory.Domain.Entities
{
    public class ReagentReceipt
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int SupplierId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string? DocumentNumber { get; set; }

        public Reagent Reagent { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
    }
}
