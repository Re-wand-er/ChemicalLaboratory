using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class ReagentSupplier
    {
        [Key]
        public int IdReagSupp { get; set; }
        public int? IdSupplier { get; set; }
        public int IdReagent { get; set; }
        public DateTime DateOfDelivary { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Reagent Reagent { get; set; }
    }
}