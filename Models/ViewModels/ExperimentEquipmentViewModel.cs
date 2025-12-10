using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ExperimentEquipmentViewModel
    {
        [Key]
        public int IdExpEq { get; set; }
        public int IdExperiment { get; set; }
        public int IdEquipment { get; set; }

        public virtual ExperimentViewModel Experiment { get; set; }
        public virtual EquipmentViewModel Equipment { get; set; }
    }
}
