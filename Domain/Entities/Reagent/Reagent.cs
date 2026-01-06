using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Entities
{
    public class Reagent
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ChemicalFormula { get; set; }
        public string Unit { get; set; } = null!;
        public decimal CurrentQuantity { get; set; }
        public decimal MinQuantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? StorageLocation { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ReagentCategory Category { get; set; } = null!;
        public ICollection<ReagentOperation> Operations { get; set; } = new List<ReagentOperation>();
        public ICollection<ReagentReceipt> Receipts { get; set; } = new List<ReagentReceipt>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
