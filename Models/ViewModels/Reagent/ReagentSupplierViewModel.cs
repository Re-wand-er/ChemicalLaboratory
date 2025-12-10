using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ReagentSupplierViewModel
    {
        [Key]
        public int IdReagSupp { get; set; }
        public int? IdSupplier { get; set; }
        public int IdReagent { get; set; }
        public DateTime DateOfDelivary { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        //public virtual SupplierViewModel Supplier { get; set; }
        public virtual ReagentViewModel Reagent { get; set; }
    }
}