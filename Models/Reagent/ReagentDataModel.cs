using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.Reagent
{
    public class ReagentDataModel
    {
        public int idReagent {  get; set; }
        [Range(0, double.MaxValue)]
        public decimal Dansity { get; set; }        
        public string? Name { get; set; }           
        public string? ChemicalFormula { get; set; }
        [Range(0,100, ErrorMessage ="Качество должно находится в диапазоне от 0 до 100!")]
        public string? Purity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Mass { get; set; }    
        
    }
}
