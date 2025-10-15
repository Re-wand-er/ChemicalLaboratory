using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class WorkSchedule
    {
        public int idWorkSchedule { get; set; }
        public string WorkShift { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime {get; set;}

        public ICollection<People> Peoples { get; set; } = new List<People>(); 
    }
}
