using ChemicalLaboratory.Models.Reagent;

namespace ChemicalLaboratory.Models
{
    public class ReagentExperiment
    {
        public int idReagentExperiment { get; set; } = 0;
        //public ExperimentDataModel idExperimentDataModel { get; set; } = new ExperimentDataModel();
        public ReagentManufacturer idReagentDataModel { get; set; } = new ReagentManufacturer();
        public int UseCount { get; set; } = 0;
        public double Mass { get; set; } = double.NaN;

        public ReagentExperiment() { }
    }
}
