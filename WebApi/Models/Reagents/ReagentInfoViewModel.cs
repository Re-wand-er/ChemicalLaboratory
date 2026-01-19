using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Models.Reagents
{
	public class ReagentInfoViewModel
	{
		public ReagentViewModel? Reagents { get; set; }
		public ReagentSupplierViewModel? Suppliers { get; set; }
		public ReagentManufacturerViewModel? Manufacturers { get; set; }
	}
}
