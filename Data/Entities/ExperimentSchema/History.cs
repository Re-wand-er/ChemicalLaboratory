using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class History
    {
        public int idHistory { get; set; }
	    public int idExperiment { get; set; }
        public string UserName { get; set; } = null!;
	    public string? OperationType { get; set; }// check(OperationType in ('Insert','Update','Delete')),
	    public DateTime LastUpdateDate { get; set; }
        
        public Experiment? Experiment { get; set; }
    }
}
