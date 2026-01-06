namespace ChemicalLaboratory.Domain.Entities
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool AffectsQuantity { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ReagentOperation> ReagentOperations { get; set; } = new List<ReagentOperation>();

    }
}
