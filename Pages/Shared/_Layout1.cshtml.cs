using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChemicalLaboratory.Models.People;

namespace ChemicalLaboratory.Pages.Shared
{
    public class _Layout1Model : PageModel
    {
        public string UserHtmlPanel { get; set; } = "/Home/Authorisation";
        public void OnGet()
        {

        }
    }

}
