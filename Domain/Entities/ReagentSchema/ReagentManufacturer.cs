using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ReagentManufacturer
    {
        [Key]
        public int IdReagManuf { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public string Series { get; set; } = null!;
        public decimal PurityDegree { get; set; }

        public int? idManufacturer { get; set; }
        public int idReagent { get; set; }
        public int idPurity { get; set; }

        public Manufacturer? Manufacturer { get; set; } = new();
        public Reagent Reagent { get; set; } = null!;
        public Purity? Purity { get; set; } = null!;
    }
}
