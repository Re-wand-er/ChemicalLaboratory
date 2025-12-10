using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Purity
    {
        [Key]
        public int idPurity { get; set; }

        [MaxLength(25)]
        public string Classification { get; set; } = null!;

        public List<ReagentManufacturer>? ReagentManufacturer { get; set; }
    }
}
