using ChemicalLaboratory.Models.Experiment;
using ChemicalLaboratory.Models.Equipment;

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
