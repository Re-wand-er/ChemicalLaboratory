using Microsoft.AspNetCore.Mvc.RazorPages;

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
