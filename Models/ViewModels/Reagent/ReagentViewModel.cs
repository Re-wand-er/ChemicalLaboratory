
using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ReagentViewModel
    {

        [Key]
        public int IdReagent { get; set; }
        public decimal Dansity { get; set; }
        public string? Name { get; set; }
        public string? ChemicalFormula { get; set; }
        public decimal mass { get; set; }

        //public virtual ICollection<ReagentSupplier> ReagentSuppliers { get; set; }
        //public virtual ICollection<ReagentManufacturer> ReagentManufacturers { get; set; }
        //public virtual ICollection<ReagentExperiment> ReagentExperiments { get; set; }
    }
}
