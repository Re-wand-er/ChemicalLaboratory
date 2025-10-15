using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class ManufacturerEquip
    {
        // переделать класс как дополняемый
        public int idManufacturer { get; set; }
        public string Email { get; set; } = null!;
        //check(PhoneNumber like '^\+?[0-9]{1,3}?[ ]?(\([0-9]{1,4}\)[ ]?)?[0-9\- ]{5,15}$'),
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Country { get; set; } = null!;

        public ICollection<EquipmentManufacturer> EquipmentManufacturers { get; set; } = new List<EquipmentManufacturer>();
    }
}
