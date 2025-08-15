using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class ExperimentEquipment
    {
        [Key]
        public int IdExpEq { get; set; }
        public int IdExperiment { get; set; }
        public int IdEquipment { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
