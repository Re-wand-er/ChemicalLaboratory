using ChemicalLaboratory.WebUI.Models.Peoples;
using Domain.DTOs;
using Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebUI.Controllers
{
	//[Authorize(Roles = "Администратор")]
	public class PeopleController : Controller
	{
		private readonly IPeopleRepository _peopleService;

		public PeopleController(IPeopleRepository peopleService)
		{
			_peopleService = peopleService;
		}

		[HttpGet]
		public IActionResult Index() { return View("People", new PeopleListViewModel()); }

		//[HttpGet] Временно вместо ного другой index
		public async Task<IActionResult> Index(
			string? searchQuery,
			int filterCategory = 0,
			int orderBy = 0,
			bool ascending = true,
			int? delete = null)
		{
			if (delete != null)
				await _peopleService.DeleteAsync(delete.Value);

			var people = await _peopleService.GetAllAsync();
			var mapped = people.ConvertAll(dto => new PeopleViewModel(dto));

			// сортировка
			mapped = ListOrder(mapped, orderBy, ascending);

			// поиск
			if (!string.IsNullOrWhiteSpace(searchQuery))
			{
				mapped = ApplySearch(mapped, searchQuery, filterCategory);
			}

			ViewBag.SearchQuery = searchQuery;
			ViewBag.FilterCategory = filterCategory;
			ViewBag.OrderBy = orderBy;
			ViewBag.Ascending = ascending;

			return View(mapped);
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromBody] PeopleDTO updated)
		{
			await _peopleService.UpdateAsync(updated);
			return Ok();
		}

		private List<PeopleViewModel> ApplySearch(List<PeopleViewModel> list, string q, int category)
		{
			q = q.Trim();

			return category switch
			{
				1 => list.Where(i => i.FirstName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList(),
				2 => list.Where(i => i.MiddleName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList(),
				3 => list.Where(i => i.LastName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList(),
				4 => list.Where(i => i.JobPosition.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList(),
				_ => list.Where(i =>
							i.FirstName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
							i.LastName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
							i.MiddleName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
							i.JobPosition.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList()
			};
		}

		private List<PeopleViewModel> ListOrder(List<PeopleViewModel> list, int order, bool asc)
		{
			return order switch
			{
				1 => Sort(list, p => p.FirstName, asc),
				2 => Sort(list, p => p.MiddleName, asc),
				3 => Sort(list, p => p.LastName, asc),
				4 => Sort(list, p => p.Sex, asc),
				5 => Sort(list, p => p.Email, asc),
				6 => Sort(list, p => p.JobPosition, asc),
				7 => Sort(list, p => p.WorkSchedule?.WorkShift, asc),
				8 => Sort(list, p => p.WorkSchedule?.StartTime, asc),
				9 => Sort(list, p => p.WorkSchedule?.EndTime, asc),
				10 => Sort(list, p => p.SystemRole, asc),
				11 => Sort(list, p => p.Login, asc),
				_ => list
			};
		}

		private List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> key, bool asc)
		{
			return asc ? list.OrderBy(key).ToList() : list.OrderByDescending(key).ToList();
		}
	}
}
