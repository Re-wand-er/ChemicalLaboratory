namespace ChemicalLaboratory.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }

        public ICollection<ReagentReceipt> ReagentReceipts { get; set; } = new List<ReagentReceipt>();
    }
}
