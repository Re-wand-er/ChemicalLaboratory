using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class ReagentExperiment
    {
        [Key]
        public int IdReagExpetiment { get; set; }
        public int? IdExperiment { get; set; }
        public int? IdReagent { get; set; }
        public int? UseCount { get; set; }
        public decimal Mass { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual Reagent Reagent { get; set; }
    }
}