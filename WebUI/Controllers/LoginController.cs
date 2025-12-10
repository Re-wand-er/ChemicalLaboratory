using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index() 
        {
            return View();
        }
    }
}
