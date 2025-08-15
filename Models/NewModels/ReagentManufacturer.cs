using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class ReagentManufacturer
    {
        [Key]
        public int IdReagManuf { get; set; }
        public int? IdManufacurer { get; set; }
        public int IdReagent { get; set; }
        public int DateOfManufacture { get; set; }
        public string Series { get; set; }
        public int IdPurity { get; set; }

        public virtual Reagent Reagent { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Purity Purity { get; set; }
    }
}