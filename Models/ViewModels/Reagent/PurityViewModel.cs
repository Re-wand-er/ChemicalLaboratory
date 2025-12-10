using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class PurityViewModel
    {
        [Key]
        public int IdPurity { get; set; }
        public string Classification { get; set; }

        public virtual ICollection<ReagentManufacturerViewModel> ReagentManufacturers { get; set; }
    }
}