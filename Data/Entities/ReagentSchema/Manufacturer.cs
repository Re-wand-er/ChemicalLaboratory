using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class Manufacturer
    {
        public int idManufacturer { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public ICollection<ReagentManufacturer> ReagentManufacturers { get; set; } = new List<ReagentManufacturer>();
    }
}
