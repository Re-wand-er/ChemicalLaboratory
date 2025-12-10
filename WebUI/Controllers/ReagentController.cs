using ChemicalLaboratory.WebUI.Models.Reagents;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Controllers
{
	public class ReagentController : Controller
	{
		[HttpGet]
		public IActionResult Index() 
		{
			return View("Reagent", new ReagentListViewModel()); 
		}
	}
}
