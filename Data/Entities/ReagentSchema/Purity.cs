using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
