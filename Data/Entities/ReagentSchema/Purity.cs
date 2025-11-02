using System.ComponentModel.DataAnnotations;

namespace EFCore.Entities
{
    internal class Purity
    {
        [Key]
        public int idPurity { get; set; }

        [MaxLength(25)]
        public string Classification { get; set; } = null!;

        public List<ReagentManufacturer>? ReagentManufacturer { get; set; }
    }
}
