using ChemicalLaboratory.WebUI.Models.Equipments;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Controllers
{
	public class EquipmentController : Controller
	{

		[HttpGet]
		public IActionResult Index() 
		{
			return View("Equipment", new EquipmentViewModel()); 
		}
	}
}
