using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
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
