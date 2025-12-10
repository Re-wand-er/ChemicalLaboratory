namespace ChemicalLaboratory.WebUI.Models.Reagents  
{
    public class ReagentManufacturerViewModel
    {
        public int IdReagManuf { get; set; }
        public int? IdManufacurer { get; set; }
        public int IdReagent { get; set; }
        public int DateOfManufacture { get; set; }
        public string Series { get; set; } = null!;
        public int IdPurity { get; set; }
    }
}