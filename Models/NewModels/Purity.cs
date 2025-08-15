using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class Purity
    {
        [Key]
        public int IdPurity { get; set; }
        public string Classification { get; set; }

        public virtual ICollection<ReagentManufacturer> ReagentManufacturers { get; set; }
    }
}