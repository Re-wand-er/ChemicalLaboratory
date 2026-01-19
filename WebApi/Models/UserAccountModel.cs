using ChemicalLaboratory.WebApi.Models.Peoples;
using ChemicalLaboratory.WebApi.Models.Equipments;

namespace ChemicalLaboratory.WebApi.Models
{
    public class UserAccountModel
    {
        public PeopleViewModel User { get; set; } = new PeopleViewModel();
        //public ExperimentViewModel Experiment { get; set; } = new ExperimentViewModel();
        //public IEnumerable<ExperimentViewModel> Experiments { get; set; } = [];


        //public IEnumerable<EquipmentViewModel> Equipment { get; set; } = [];
        //public IEnumerable<ReagentExperiment> Reagent { get; set; } = [];
    }
}
