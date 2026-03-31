using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/reagent")]
	public class ReagentController : ControllerBase
	{
		private readonly ReagentService _reagentService;
		private readonly ILogger<ReagentController> _logger;

		public ReagentController(ReagentService reagentService, ILogger<ReagentController> logger)
		{
			_reagentService = reagentService;
			_logger = logger;
		}

		[HttpGet] public async Task<IActionResult> GetAllReagents() 
		{
			var result = await _reagentService.GetAllAsync();

            return Ok(result);
		}

		[HttpGet("{id:int}")] public async Task<IActionResult> GetReagentById(int id) => Ok(await _reagentService.GetByIdAsync(id));

		[HttpPost]
		public async Task<IActionResult> AddReagent([FromBody] ReagentDTO dto) 
		{
			_logger.LogInformation("Creating Reagent in controller");

			await _reagentService.AddAsync(dto);
			return Ok(new { succes = true });
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteReagent(int id) 
		{
            _logger.LogInformation($"Deleted reagent with id = {id} in controller");

            await _reagentService.DeleteAsync(id);
			return Ok(new { succes = true });
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateReagent([FromBody] ReagentDTO reagentDTO) 
		{
            _logger.LogInformation($"Updated notification with id = {reagentDTO.Id} in controller");

            await _reagentService.UpdateAsync(reagentDTO);
            return Ok(new { succes = true });
        }
	}
}
