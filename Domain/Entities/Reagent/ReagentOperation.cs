using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Domain.Entities
{
    public class ReagentOperation
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int UserId { get; set; }
        public int OperationTypeId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OperationDate { get; set; }
        public string? Comment { get; set; }

        public Reagent Reagent { get; set; } = null!;
        public User User { get; set; } = null!;
        public OperationType OperationType { get; set; } = null!;
    }
}
