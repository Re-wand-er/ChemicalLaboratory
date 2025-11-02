using ChemicalLaboratory.Models.Equipment;

namespace ChemicalLaboratory.Models.Experiment
{
    public class ExperimentDataModel
    {
        public int idExperiment { get; set; } = 0;
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MinValue;
        public string? Result { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public List<EquipmentDataModel> idEquipmentDataModel { get; set; } = new List<EquipmentDataModel>();
        public List<ReagentExperiment> idReagentDataModel { get; set; } = new List<ReagentExperiment>();

        public ExperimentDataModel() { }
    }
}
