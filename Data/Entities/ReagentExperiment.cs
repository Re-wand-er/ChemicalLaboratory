using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class ReagentExperiment
    {
        public int idReagExpetiment { get; set; }
		public int? idExperiment { get; set; }
        public int? idReagent { get; set; }
        public int? UseCount { get; set; }
        public decimal? Mass { get; set; }

        public Reagent? Reagent { get; set; }
        public Experiment? Experiment { get; set; }
    }
}
