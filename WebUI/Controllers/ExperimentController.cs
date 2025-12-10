using ChemicalLaboratory.WebUI.Models.Experiments;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.Controllers
{
    //[Authorize]
    public class ExperimentController : Controller
    {
        //private readonly IExperimentRepository _experimentService;

        //public ExperimentController(IExperimentRepository experimentService)
        //{
        //    _experimentService = experimentService;
        //}

        [HttpGet]
        public IActionResult Index() 
        {
            
            return View("Experiment", new ExperimentListViewModel());
        }

        //public async Task<IActionResult> OldIndex(
        //    string? searchQuery,
        //    int filterCategory,
        //    int orderBy,
        //    bool ascending = true,
        //    int? update = null,
        //    int? delete = null)
        //{
        //    if (update != null)
        //    {
        //        //JsonRequest.Free();
        //        JsonRequest.Instance(update.Value);
        //        return RedirectToAction("Update", "Experiment");
        //    }
        //
        //    if (delete != null)
        //    {
        //        //await _experimentService.DeleteAsync(delete.Value);
        //    }
        //
        //    var experimentList = Map(await _experimentService.GetAllAsync());
        //    experimentList = Sort(experimentList, orderBy, ascending);
        //    
        //    if (!string.IsNullOrWhiteSpace(searchQuery))
        //    {
        //        experimentList = Filter(experimentList, searchQuery, filterCategory);
        //    }
        //    
        //    return View(experimentList);
        //}

        //private List<ExperimentViewModel> Map(List<ExperimentDTO> dto)
        //{
        //    return dto.ConvertAll(d => new ExperimentViewModel(d));
        //}

        private List<ExperimentViewModel> Sort(
            List<ExperimentViewModel> list, int order, bool asc)
        {
            return order switch
            {
                1 => Sorter(list, p => p.Name, asc),
                2 => Sorter(list, p => p.Description, asc),
                3 => Sorter(list, p => p.StartDate, asc),
                4 => Sorter(list, p => p.EndDate, asc),
                5 => Sorter(list, p => p.Result, asc),
                6 => Sorter(list, p => p.Status, asc),
                _ => list
            };
        }

        private List<T> Sorter<T, TKey>(
            List<T> list, Func<T, TKey> selector, bool asc)
        {
            return asc
                ? list.OrderBy(selector).ToList()
                : list.OrderByDescending(selector).ToList();
        }

        private List<ExperimentViewModel> Filter(
            List<ExperimentViewModel> list, string q, int category)
        {
            q = q.ToLower();

            return category switch
            {
                1 => list.Where(i => i.Name?.ToLower().Contains(q) == true).ToList(),
                2 => list.Where(i => i.Description?.ToLower().Contains(q) == true).ToList(),
                3 => list.Where(i => i.StartDate?.ToString()?.Contains(q) == true).ToList(),
                4 => list.Where(i => i.EndDate?.ToString()?.Contains(q) == true).ToList(),
                5 => list.Where(i => i.Result?.ToLower().Contains(q) == true).ToList(),
                6 => list.Where(i => i.Status?.ToLower().Contains(q) == true).ToList(),
                _ => list
            };
        }
    }
}
