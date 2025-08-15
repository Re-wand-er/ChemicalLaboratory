using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }
        public int? IdManufacturer { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<ReagentSupplier> ReagentSuppliers { get; set; }
    }
}