using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChemicalLaboratory.Pages.Home
{
    public class AutomatizationModel : PageModel
    {
        [BindProperty(SupportsGet=true)]
        public string ChemicalFormula { get; set; } = string.Empty;
        [BindProperty]
        public double Concentration { get; set; } = double.NaN;
        [BindProperty]
        public string Volume { get; set; } = string.Empty;
        public double Massa { get; set; } = double.NaN;
        public string WaterVolume { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPostCalculate()
        {
            
            Massa = 54;
        }


        
    }
}
