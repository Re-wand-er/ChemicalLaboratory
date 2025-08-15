using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class Manufacturer
    {
        [Key]
        public int IdManufacturer { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<ReagentManufacturer> ReagentManufacturers { get; set; }
    }
}