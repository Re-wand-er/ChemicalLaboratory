using ChemicalLaboratory.Models.ViewModels;
using EFCore.DTOs;
using EFCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChemicalLaboratory.Pages.Home
{
    [Authorize(Roles = "Администратор")]
    public class PeopleModel : PageModel
    {
        private readonly IPeopleService _peopleService;

        public PeopleModel(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [BindProperty]
        public List<PeopleViewModel>? PeopleList { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int FilterCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        public async Task OnGet()
        {

            if (Request.Query.TryGetValue("Delete", out var deleteValue))
            {
                if (int.TryParse(deleteValue, out var reagentId))
                {
                    await _peopleService.DeleteAsync(reagentId);
                }
            }

            PeopleList = PepleDataMapping(await _peopleService.GetAllAsync());
            ListOrder(OrderBy, Ascending);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                switch (FilterCategory)
                {
                    case 1:
                        PeopleList = PeopleList?.Where(i => i.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 2:
                        PeopleList = PeopleList?.Where(i => i.MiddleName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 3:
                        PeopleList = PeopleList?.Where(i => i.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 4:
                        PeopleList = PeopleList?.Where(i => i.JobPosition.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    default:
                        // Если категория не выбрана, фильтруем по всем полям
                        PeopleList = PeopleList?.Where(i =>
                            i.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                            i.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                            i.MiddleName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                            i.JobPosition.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                        ).ToList();
                        break;
                }
                //ReagentList = ReagentList.Where(i => i.Mass.ToString("F2").Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public async Task<IActionResult> OnPost([FromBody] PeopleDTO updatedPeople)
        {
            await _peopleService.UpdateAsync(updatedPeople);

            return Page();
        }

        public IActionResult ListOrder(int Order, bool ascending)
        {
            switch (Order)
            {
                case 1:
                    PeopleList = SortList(PeopleList, p => p.FirstName, ascending);
                    break;

                case 2:
                    PeopleList = SortList(PeopleList, p => p.MiddleName, ascending);
                    break;

                case 3:
                    PeopleList = SortList(PeopleList, p => p.LastName, ascending);
                    break;

                case 4:
                    PeopleList = SortList(PeopleList, p => p.Sex, ascending);
                    break;

                case 5:
                    PeopleList = SortList(PeopleList, p => p.Email, ascending);
                    break;

                case 6:
                    PeopleList = SortList(PeopleList, p => p.JobPosition, ascending);
                    break;

                case 7:
                    PeopleList = SortList(PeopleList, p => p.WorkSchedule?.WorkShift, ascending);
                    break;

                case 8:
                    PeopleList = SortList(PeopleList, p => p.WorkSchedule?.StartTime, ascending);
                    break;

                case 9:
                    PeopleList = SortList(PeopleList, p => p.WorkSchedule?.EndTime, ascending);
                    break;

                case 10:
                    PeopleList = SortList(PeopleList, p => p.SystemRole, ascending);
                    break;

                case 11:
                    PeopleList = SortList(PeopleList, p => p.Login, ascending);
                    break;

                default: break;
            }

            return Page();
        }

        static List<T> SortList<T, TKey>(List<T> list, Func<T, TKey> keySelector, bool ascending = true)
        {
            return ascending ? list.OrderBy(keySelector).ToList() : list.OrderByDescending(keySelector).ToList();
        }

        private List<PeopleViewModel> PepleDataMapping(List<PeopleDTO> peopleDTO)
        {
            return peopleDTO.ConvertAll(dto => new PeopleViewModel(dto));
        }
    }
}
