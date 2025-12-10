namespace ChemicalLaboratory.WebUI.Models.Reagents
{
    public class ReagentViewModel
    {
        public int IdReagent { get; set; }
        public decimal Dansity { get; set; }
        public string? Name { get; set; }
        public string? ChemicalFormula { get; set; }
        public decimal Mass { get; set; }
    }
}
