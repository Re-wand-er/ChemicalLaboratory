using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChemicalLaboratory.Models.Reagent;
using Reag = ChemicalLaboratory.Models.NewModels.Reagent;
using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models.People;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ChemicalLaboratory.Domain.ORM;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace ChemicalLaboratory.Pages.Home
{
    //[ValidateAntiForgeryToken]
    //[IgnoreAntiforgeryToken]
    [Authorize]
	public class ReagentModel : PageModel
    {

        [BindProperty]
        public List<ReagentManufacturer>? ReagentManufacturers { get; set; }
        //public List<Reag>? ReagentManufacturers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool isReagentVisible { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool isManufactureVisible { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public bool isSupplierVisible { get; set; } = false;
		[BindProperty(SupportsGet = true)]
		public int FilterCategory { get; set; }

		[BindProperty(SupportsGet = true)]
        public int OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        int ParamOfKind = 1; // 1 - реагенты; // 2 - производство // 3 - поставщики // 4 - реагенты и производство // 5 - производство и поставщик
                             // 6 - реагенты и поставщик // 7 - реагенты, производство и поставщик (в общем все)


        [BindProperty]
        public Dictionary<int, ReagentDataModel> UpdatedItems { get; set; } = new();

        public async /*IActionResult*/  void OnGet(string searchQuery)
        {
            string? sqlCommand = null;
            ReagentManufacturers = new List<ReagentManufacturer>();

			if (Request.Query.TryGetValue("Delete", out var deleteValue))
            {
                if (int.TryParse(deleteValue, out var reagentId))
                {
                    switch (ParamOfKind)
                    {
                        case 1: case 4: case 6: case 7: { SQLCommand.DeleteRecord("DELETE FROM ReagentSchema.ReagentManufacturer WHERE IdReagManuf = @Id", reagentId); break; }
                        case 2: case 5: { SQLCommand.DeleteRecord("DELETE FROM ReagentSchema.Manufacturer WHERE IdManufacturer = @Id", reagentId); break; }
                        case 3: { SQLCommand.DeleteRecord("DELETE FROM ReagentSchema.Supplier WHERE idSupplier = @Id", reagentId); break; }
                        default: break;
                    }

                    //SQLCommand.DeleteRecord(reagentId);
                }
            }

            if (isReagentVisible && isManufactureVisible && isSupplierVisible) 
            {
                ParamOfKind = 7;
                sqlCommand = "select \r\n\t*, \r\n\trsr.Name  as ReagentName, \r\n\trsm.Name  as ManufactureName, \r\n\trsm.email as ManufactureEmail,\r\n\trss.Name  as SupplierName,\r\n\trss.email as SupplierEmail\r\n\tfrom ReagentSchema.Reagent rsr\r\njoin ReagentSchema.ReagentManufacturer rsrm on rsr.idReagent = rsrm.idReagent\r\njoin ReagentSchema.Purity rsp on rsrm.idPurity = rsp.idPurity\r\njoin ReagentSchema.Manufacturer as rsm on rsrm.idManufacturer = rsm.IdManufacturer\r\njoin ReagentSchema.Supplier rss on rss.idManufacturer = rsm.IdManufacturer \r\n";
            }
            else if (isReagentVisible && isSupplierVisible)
            {
                ParamOfKind = 6;
                sqlCommand = "select \r\n\trsr.idReagent, rsr.Dansity, rsr.Name as ReagentName, rsr.ChemicalFormula, rsr.mass, \r\n\trsrm.DateOfManufacture, rsrm.series, rsp.Classification, rsrm.PurityDegree,\r\n\trss.idSupplier, rss.email as SupplierEmail, rss.Name  as SupplierName, rss.PhoneNumber\r\n\tfrom ReagentSchema.Reagent rsr\r\njoin ReagentSchema.ReagentManufacturer rsrm on rsr.idReagent = rsrm.idReagent\r\njoin ReagentSchema.Purity rsp on rsrm.idPurity = rsp.idPurity\r\njoin ReagentSchema.Manufacturer as rsm on rsrm.idManufacturer = rsm.IdManufacturer\r\njoin ReagentSchema.Supplier rss on rss.idManufacturer = rsm.IdManufacturer ";
            }
            else if (isManufactureVisible && isSupplierVisible)
            {
                ParamOfKind = 5;
                sqlCommand = "select \r\n\t*, \r\n\trsm.Name  as ManufactureName, \r\n\trsm.email as ManufactureEmail,\r\n\trss.Name  as SupplierName,\r\n\trss.email as SupplierEmail \r\nfrom ReagentSchema.Manufacturer rsm \r\njoin ReagentSchema.Supplier rss on rsm.IdManufacturer = rss.idManufacturer ";
            }
            else if (isReagentVisible && isManufactureVisible)
            {
                ParamOfKind = 4;
                sqlCommand = "select *, \r\n\trsr.Name  as ReagentName, \r\n\trsm.Name  as ManufactureName, \r\n\trsm.email as ManufactureEmail\r\nfrom ReagentSchema.Reagent rsr\r\njoin ReagentSchema.ReagentManufacturer rsrm on rsr.idReagent = rsrm.idReagent\r\njoin ReagentSchema.Purity rsp on rsp.idPurity = rsrm.idPurity\r\njoin ReagentSchema.Manufacturer rsm on rsrm.idManufacturer = rsm.IdManufacturer";
            }
            else if (isSupplierVisible)
            {
                ParamOfKind = 3;
                sqlCommand = "select \r\n\t*, \r\n\trss.Name  as SupplierName,\r\n\trss.email as SupplierEmail \r\nfrom ReagentSchema.Supplier as rss";
            }
            else if (isManufactureVisible)
            {
                ParamOfKind = 2;
                sqlCommand = "select \r\n\t*, \r\n\trsm.Name  as ManufactureName, \r\n\trsm.email as ManufactureEmail\r\nfrom ReagentSchema.Manufacturer as rsm";
            }
            else if (isReagentVisible)
            {
                ParamOfKind = 1;
                sqlCommand = "select *, rsr.Name  as ReagentName from ReagentSchema.Reagent rsr join ReagentSchema.ReagentManufacturer rsrm on rsr.idReagent = rsrm.idReagent join ReagentSchema.Purity rsp on rsp.idPurity = rsrm.idPurity";
            }

            if (sqlCommand is not null) 
            {
                ReagentManufacturers = LoadItems(sqlCommand);
                ListOrder(OrderBy, Ascending);
                Filter(SearchQuery);
                //return Page();
            }

            /*if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
               ReagentManufacturers = ReagentManufacturers.Where(i => i.Reagent.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }*/
            ListOrder(OrderBy, Ascending);
            Filter(SearchQuery);

            //foreach (var i in  await NewLoadItems())
            //    Console.WriteLine();

            //return new JsonResult(new { success = false });
        }

        public async Task<IActionResult> OnPost([FromBody] ReagentDataModel updatedReagent)
        {
           await SQLCommand.UpdateReagentRecord(updatedReagent);

           return new JsonResult(new { success = true });
        }

        //private async Task<IEnumerable<Reag>> NewLoadItems()
        //{
        //    return await _reagentRepository.GetReagent(); 
        //}

        private List<ReagentManufacturer/*ReagentDataModel*/> LoadItems( string query)
        {
           return SQLCommand.GetDataFromReagentSchema(query); // Метод для получения данных из базы
        }

        public IActionResult Filter(string? filt)
        {
			if (!string.IsNullOrWhiteSpace(SearchQuery))
			{
				switch (FilterCategory)
				{
					/*case 1:
						ReagentManufacturers = ReagentManufacturers?.Where(i => i.Reagent.Dansity.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
						break;*/

					case 2:
						ReagentManufacturers = ReagentManufacturers?.Where(i => i.Reagent.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
						break;

					case 3:
						ReagentManufacturers = ReagentManufacturers?.Where(i => i.Reagent.ChemicalFormula.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
						break;

					/*case 4:
						ReagentManufacturers = ReagentManufacturers?.Where(i => i.Reagent.Mass.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
						break;*/

                    case 5:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.PurityClassification.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    /*case 6:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.PurityDegree.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;*/

                    case 7:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Manufacturer.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 8:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Manufacturer.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 9:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Manufacturer.Address.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 10:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Manufacturer.City.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 11:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Manufacturer.Country.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 12:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Supplier.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 13:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Supplier.email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;
                    case 14:
                        ReagentManufacturers = ReagentManufacturers?.Where(i => i.Supplier.PhoneNumber.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    default: break;
				}

			}

			return Page();
        }

		public IActionResult ListOrder(int Order, bool ascending)
		{
			switch (Order)
			{
				case 1:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Reagent.Dansity, ascending);
					break;

				case 2:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Reagent.Name, ascending);
					break;

				case 3:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Reagent.ChemicalFormula, ascending);
					break;

				case 4:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Reagent.Mass, ascending);
					break;

				case 5:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.PurityClassification, ascending);
					break;

				case 6:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.PurityDegree, ascending);
					break;

				case 7:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Manufacturer.Name, ascending);
					break;

				case 8:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Manufacturer.Email, ascending);
					break;

				case 9:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Manufacturer.Address, ascending);
					break;

				case 10:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Manufacturer.City, ascending);
					break;

				case 11:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Manufacturer.Country, ascending);
					break;

				case 12:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Supplier.Name, ascending);
					break;

				case 13:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Supplier.email, ascending);
					break;

				case 14:
					ReagentManufacturers = SortList(ReagentManufacturers, p => p.Supplier.PhoneNumber, ascending);
					break;


				default: break;
			}

			return Page();
		}

		static List<T> SortList<T, TKey>(List<T>? list, Func<T, TKey> keySelector, bool ascending = true)
        {
            return ascending ? list!.OrderBy(keySelector).ToList() : list!.OrderByDescending(keySelector).ToList();
        }
    }
}
