using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Models.Experiments
{
    public class ExperimentListViewModel
    {
        public UserControlModel UserControl { get; set; } = new UserControlModel();
        public IEnumerable<ExperimentViewModel> Experiments { get; set; } = [];
    }
}
