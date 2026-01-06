namespace ChemicalLaboratory.Domain.Entities
{
    public class ReagentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Reagent> Reagents { get; set; } = new List<Reagent>();
    }
}
