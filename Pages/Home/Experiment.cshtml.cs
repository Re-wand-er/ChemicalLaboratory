using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models.Experiment;
using ChemicalLaboratory.Models.ViewModels;
using EFCore.DTOs;
using EFCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChemicalLaboratory.Pages.Home
{
    [Authorize]
    public class ExperimentModel : PageModel
    {
        private readonly IExperimentService _experimentService;
        public ExperimentModel(IExperimentService experimentService) { _experimentService = experimentService; }
        [BindProperty]
        public List<ExperimentViewModel>? Experiments { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FilterCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Ascending { get; set; } = true;

        public async Task<IActionResult> OnGet()
        {
            if (Request.Query.TryGetValue("Update", out var updateValue))
            {
                if (int.TryParse(updateValue, out var id))
                {
                    JsonRequest.InstanceFree();
                    JsonRequest.Instance(id);
                    return RedirectToPage("/Add/UpdateExperiment"/*, new { id }*/);
                }
            }

            if (Request.Query.TryGetValue("Delete", out var deleteValue))
            {
                if (int.TryParse(deleteValue, out var id))
                {
                    await _experimentService.DeleteAsync(id);
                }
            }

            var experimentList = ExperimentDataMapping(await _experimentService.GetAllAsync());
            Experiments = experimentList;

            ListOrder(OrderBy, Ascending);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                switch (FilterCategory)
                {
                    case 1:
                        Experiments = Experiments?.Where(i => i.Name != null && i.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 2:
                        Experiments = Experiments?.Where(i => i.Description != null && i.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        break;

                    case 3:
                        Experiments = Experiments?.Where(i => i.StartDate?.ToString()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
                        break;

                    case 4:
                        Experiments = Experiments?.Where(i => i.EndDate?.ToString()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
                        break;

                    case 5:
                        Experiments = Experiments?.Where(i => i.Result?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
                        break;

                    case 6:
                        Experiments = Experiments?.Where(i => i.Status?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
                        break;

                    default:
                        // Если категория не выбрана, фильтруем по всем полям
                        //Experiments = Experiments?.Where(i =>
                        //    (i.Name != null && i.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                        //    (i.Description != null && i.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
                        //    (i.StartDate?.ToString()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        //    (i.EndDate?.ToString()?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        //    (i.Result?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        //    (i.Status?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false)
                        //).ToList();

                        break;
                }
            }

            return Page();
        }

        public IActionResult ListOrder(int Order, bool ascending)
        {


            switch (Order)
            {
                case 1:
                    Experiments = SortList(Experiments, p => p.Name, ascending);
                    break;

                case 2:
                    Experiments = SortList(Experiments, p => p.Description, ascending);
                    break;

                case 3:
                    Experiments = SortList(Experiments, p => p.StartDate, ascending);
                    break;

                case 4:
                    Experiments = SortList(Experiments, p => p.EndDate, ascending);
                    break;

                case 5:
                    Experiments = SortList(Experiments, p => p.Result, ascending);
                    break;

                case 6:
                    Experiments = SortList(Experiments, p => p.Status, ascending);
                    break;

                default: break;
            }

            return Page();
        }
        static List<T> SortList<T, TKey>(List<T> list, Func<T, TKey> keySelector, bool ascending = true)
        {
            return ascending ? list.OrderBy(keySelector).ToList() : list.OrderByDescending(keySelector).ToList();
        }

        private List<ExperimentViewModel> ExperimentDataMapping(List<ExperimentDTO> experimentDTO)
        {
            return experimentDTO.ConvertAll(dto => new ExperimentViewModel(dto));
        }
    }

    public class JsonRequest
    {
        public int id { get; set; }

        public static JsonRequest? _instance;
        public static JsonRequest Instance(int idPeople)
        {
            if (_instance == null)
            {
                _instance = new JsonRequest(idPeople);
            }
            return _instance;
        }

        public static JsonRequest? InstanceFree()
        {
            _instance = null;
            return _instance;
        }
        public JsonRequest(int idJson) { id = idJson; }
    }
}
