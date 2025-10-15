using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    internal class Experiment
    {
        public int idExperiment { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } 

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Result { get; set; }

        // check (status in ('В процессе', 'Запланирован', 'Приостановлен', 'Отменен','Завершен')) 
        public string? Status { get; set; }

        public ICollection<History> ExperimentHistories { get; set; } = new List<History>();
        public ICollection<ReagentExperiment> ReagentExperiments { get; set; } = new List<ReagentExperiment>();
        public ICollection<ExperimentEquipment> ExperimentEquipments { get; set; } = new List<ExperimentEquipment>();
        
    }
}
