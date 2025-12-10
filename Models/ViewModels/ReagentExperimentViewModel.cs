using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ReagentExperimentViewModel
    {
        [Key]
        public int IdReagExpetiment { get; set; }
        public int? IdExperiment { get; set; }
        public int? IdReagent { get; set; }
        public int? UseCount { get; set; }
        public decimal Mass { get; set; }

        public virtual ExperimentViewModel Experiment { get; set; }
        public virtual ReagentViewModel Reagent { get; set; }
    }
}