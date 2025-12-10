using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ReagentManufacturerViewModel
    {
        [Key]
        public int IdReagManuf { get; set; }
        public int? IdManufacurer { get; set; }
        public int IdReagent { get; set; }
        public int DateOfManufacture { get; set; }
        public string Series { get; set; }
        public int IdPurity { get; set; }

        public virtual ReagentViewModel Reagent { get; set; }
        //public virtual ManufacturerViewModel Manufacturer { get; set; }
        public virtual PurityViewModel Purity { get; set; }
    }
}