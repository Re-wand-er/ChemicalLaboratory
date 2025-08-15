using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class Experiment
    {
        [Key]
        public int IdExperiment { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Protocol { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<ExperimentHistory> ExperimentHistories { get; set; }
        public virtual ICollection<People> People { get; set; }
        public virtual ICollection<ReagentExperiment> ReagentExperiments { get; set; }
        public virtual ICollection<ExperimentEquipment> ExperimentEquipments { get; set; }
    }
}