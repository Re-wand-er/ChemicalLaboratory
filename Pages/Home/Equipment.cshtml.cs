using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models.Equipment;
using ChemicalLaboratory.Models.ViewModels;
using ChemicalLaboratory.Models.ViewModels.Equipment;
using Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Pages.Home
{
    [Authorize]
    public class EquipmentPageModel : PageModel
    {
        //private readonly IEquipmentRepository _repository;
        //public EquipmentPageModel(IEquipmentRepository repository) { _repository = repository; }

        /////////////////////////////////////////////////////////////////////////////////////////////
        public IEnumerable<EquipmentManufacturerViewModel> EquipmentList { get; set; } = [];
        //public UserControlModel UserControl { get; set; } = new UserControlModel();
        public bool isEquipmentVisible { get; set; } = true;
        public bool isManufactureVisible { get; set; } = false;

        /// <summary>
        /// 1 - оборудование; 
        /// 2 - производство; 
        /// 3 - производство и обурадование
        /// </summary>
        public int ParamOfKind { get; set; } = 1;
        /////////////////////////////////////////////////////////////////////////////////////////////

        /*
        public List<EquipmentManufacturer>? EquipmentList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FilterCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool isEquipmentVisible { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool isManufactureVisible { get; set; } = false;

        [BindProperty]
        public int ParamOfKind { get; set; } = 1; // 1 - оборудование; // 2 - производство // 3 - производство и обурадование 
        */
        public void OnGet()
        {
            // Лучше использовать switch 
            if (isManufactureVisible)
            {
                ParamOfKind = 2;
               // EquipmentList = LoadItems();
            }

            if (isEquipmentVisible)
            {
                ParamOfKind = 1;
                //EquipmentList = LoadItems();//await _repository.GetAllEquipmentAsync();//LoadItems("select * from EquipmentSchema.Equipment");
            }

            if (isEquipmentVisible && isManufactureVisible)
            {
                ParamOfKind = 3;
                //EquipmentList = LoadItems();
            }

            //ListOrder(OrderBy, Ascending);

            /*if (!string.IsNullOrWhiteSpace(EquipmentList.UserControl.SearchQuery))
            {
                //switch (EquipmentList.UserControl.FilterCategory)
                //{
                //    case 1:
                //        EquipmentList = EquipmentList?.Where(i => i.Equipment.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 2:
                //        EquipmentList = EquipmentList?.Where(i => i.Equipment.Model.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 3:
                //        EquipmentList = EquipmentList?.Where(i => i.Equipment.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 4:
                //        EquipmentList = EquipmentList?.Where(i => i.Equipment.Kind.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 5:
                //        EquipmentList = EquipmentList?.Where(i => i.Equipment.Status.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 6:
                //        EquipmentList = EquipmentList?.Where(i => i.Manufacturer.Address.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 7:
                //        EquipmentList = EquipmentList?.Where(i => i.Manufacturer.City.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 8:
                //        EquipmentList = EquipmentList?.Where(i => i.Manufacturer.Country.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 9:
                //        EquipmentList = EquipmentList?.Where(i => i.PurchaseDate.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    case 10:
                //        EquipmentList = EquipmentList?.Where(i => i.GuaranteeDate.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                //        break;

                //    default:

                //        if (isEquipmentVisible && isManufactureVisible)
                //        {
                //            // Если категория не выбрана, фильтруем по всем полям
                //            EquipmentList = EquipmentList?.Where(i =>
                //                i.Equipment.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Model.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Kind.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Status.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.PhoneNumber.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.Address.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.City.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.Country.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.PurchaseDate.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.GuaranteeDate.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)

                //            ).ToList();

                //            break;
                //        }

                //        if (isManufactureVisible)
                //        {
                //            // Если категория не выбрана, фильтруем по всем полям
                //            EquipmentList = EquipmentList?.Where(i =>
                //                i.Manufacturer.email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.PhoneNumber.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.Address.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.City.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Manufacturer.Country.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                //            ).ToList();

                //            break;
                //        }

                //        if (isEquipmentVisible)
                //        {
                //            // Если категория не выбрана, фильтруем по всем полям
                //            EquipmentList = EquipmentList?.Where(i =>
                //                i.Equipment.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Model.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Kind.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                //                i.Equipment.Status.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                //            ).ToList();

                //            break;
                //        }
                //        break;
                //}
            }
            */
        }

        public void OnPostDelete(int id) 
        {
            if (Request.Query.TryGetValue("Delete", out var deleteValue))
            {
                if (int.TryParse(deleteValue, out var reagentId))
                {
                    switch (ParamOfKind)
                    {
                        case 1: SQLCommand.DeleteRecord("DELETE FROM EquipmentSchema.Equipment WHERE idEquipment = @Id", reagentId); break;
                        case 2: SQLCommand.DeleteRecord("DELETE FROM EquipmentSchema.Manufacturer\r\nwhere idManufacturer = @Id", reagentId); break;
                        case 3: SQLCommand.DeleteRecord("DELETE FROM EquipmentSchema.Equipment ese\r\njoin EquipmentSchema.EquipmentManufacturer esem on ese.idEquipment = esem.idEquipment\r\njoin EquipmentSchema.Manufacturer esm on esem.idManufacturer = esm.idManufacturer\r\nwhere idEquipmentManufacturer = @Id", reagentId); break;
                        default: break;
                    }

                    SQLCommand.DeleteRecord("DELETE FROM ExperimentSchema.Experiment WHERE idExperiment = @Id", reagentId);
                }
            }
        }


        private List<EquipmentManufacturer> LoadItems()
        {
            Console.WriteLine(ParamOfKind);

            return SQLCommand.GetDataFromEquipmentSchema("");
        }

        public async Task<IActionResult> OnPost([FromBody] /*EquipmentManufacturer*/ Equipment updatedReagent)
        {
            switch (updatedReagent.ModeOfUpdate)
            {
                case 1:
                case 2:
                case 3:
                    {
                        await SQLCommand.UpdateEquipmentRecord(updatedReagent, updatedReagent.ModeOfUpdate);
                        return new JsonResult(new { success = true });
                    }

            }
            // await SQLCommand.UpdateReagentRecord(updatedReagent);

            return new JsonResult(new { success = false });
        }

        public IActionResult ListOrder(int Order, bool ascending)
        {
            //switch (Order)
            //{
            //    case 1:
            //        EquipmentList = SortList(EquipmentList, p => p.Equipment.Name, ascending);
            //        break;

            //    case 2:
            //        EquipmentList = SortList(EquipmentList, p => p.Equipment.Model, ascending);
            //        break;

            //    case 3:
            //        EquipmentList = SortList(EquipmentList, p => p.Equipment.Description, ascending);
            //        break;

            //    case 4:
            //        EquipmentList = SortList(EquipmentList, p => p.Equipment.Kind, ascending);
            //        break;

            //    case 5:
            //        EquipmentList = SortList(EquipmentList, p => p.Equipment.Status, ascending);
            //        break;

            //    case 6:
            //        EquipmentList = SortList(EquipmentList, p => p.Manufacturer.Address, ascending);
            //        break;

            //    case 7:
            //        EquipmentList = SortList(EquipmentList, p => p.Manufacturer.City, ascending);
            //        break;

            //    case 8:
            //        EquipmentList = SortList(EquipmentList, p => p.Manufacturer.Country, ascending);
            //        break;

            //    case 9:
            //        EquipmentList = SortList(EquipmentList, p => p.PurchaseDate, ascending);
            //        break;

            //    case 10:
            //        EquipmentList = SortList(EquipmentList, p => p.GuaranteeDate, ascending);
            //        break;

            //    default: break;
            //}

            return Page();
        }

        static List<T> SortList<T, TKey>(List<T> list, Func<T, TKey> keySelector, bool ascending = true)
        {
            return ascending ? list.OrderBy(keySelector).ToList() : list.OrderByDescending(keySelector).ToList();
        }

    }

    public class Equipment
    {
        public int Id { get; set; }
        public int ModeOfUpdate { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? Kind { get; set; }
        public string? Status { get; set; }
        //public int?    idManufacturer { get; set; }
        [Required(ErrorMessage = "Введите электронную почту верно!")]
        [RegularExpression(
        @"^[^\s@]+@[^\s@]+\.[^\s@]+$",
        ErrorMessage = "Введите действительный адрес электронной почты.")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        //public int?    idEquipmentManufacturer { get; set; }
        public string? PurchaseDate { get; set; } // ToDateOnly
        public string? GuaranteeDate { get; set; } // ToDateOnly
    }
}
