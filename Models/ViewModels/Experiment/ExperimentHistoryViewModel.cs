using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class ExperimentHistoryViewModel
    {
        [Key]
        public int IdHistory { get; set; }
        public int IdExperiment { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Result { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Status { get; set; }
        public string Protocol { get; set; }

        public virtual ExperimentViewModel Experiment { get; set; }
    }
}
