using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class Supplier
    {
        public int idSupplier { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }


        public int? idManufacturer { get; set; }
        public Manufacturer? Manufacturer { get; set; }

    }
}
