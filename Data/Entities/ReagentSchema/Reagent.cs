namespace EFCore.Entities
{
    internal class Reagent
    {
        public int idReagent { get; set; }
        public decimal Density { get; set; }
        public string Name { get; set; } = null!;
        public string ChemicalFormula { get; set; } = null!;
        public decimal Mass { get; set; }

        public ICollection<ReagentManufacturer> ReagentManufacturers { get; set; } = new List<ReagentManufacturer>();
        public ICollection<ReagentExperiment> ReagentExperiments { get; set; } = new List<ReagentExperiment>();

    }
}
