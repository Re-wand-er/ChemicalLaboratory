using ChemicalLaboratory.Models.Equipment;
using ChemicalLaboratory.Models.Experiment;

namespace ChemicalLaboratory.Models
{
    public class ExperimentEquipment
    {
        public int idExperimentEquipment { get; set; } = 0;
        public ExperimentDataModel idExperimentDataModel { get; set; }
        public List<EquipmentDataModel> idEquipmentDataModel { get; set; }

        public ExperimentEquipment() { }
    }
}
