using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Models.Reagents
{
	public class ReagentInfoViewModel
	{
		public ReagentViewModel? Reagents { get; set; }
		public ReagentSupplierViewModel? Suppliers { get; set; }
		public ReagentManufacturerViewModel? Manufacturers { get; set; }
	}
}
