using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace ChemicalLaboratory.WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/reagent")]
	public class ReagentController : ControllerBase
	{
		private readonly ReagentService _reagentService;
		private readonly ReagentForecastService _reagentForecastService;
		private readonly ILogger<ReagentController> _logger;

		public ReagentController(ReagentService reagentService, ReagentForecastService reagentForecastService, ILogger<ReagentController> logger)
		{
			_reagentService = reagentService;
			_reagentForecastService = reagentForecastService;
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ReagentDTO>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ReagentDTO>>> GetAll([FromQuery] bool includeInactive =  false) 
		{
			var result = await _reagentService.GetAllAsync(includeInactive);
            return Ok(result);
		}

		[HttpGet("{id:int}")] public async Task<IActionResult> GetReagentById(int id) => Ok(await _reagentService.GetByIdAsync(id));

        [HttpGet("name")]
        public async Task<IActionResult> GetCategoriesName() => Ok(await _reagentService.GetAllIdNameAsync());


        [HttpPost]
		public async Task<IActionResult> AddReagent([FromBody] ReagentCreateDTO dto) 
		{
			_logger.LogInformation("Creating Reagent in controller");

			await _reagentService.AddAsync(dto.Adapt<ReagentDTO>());
			return Ok(new { succes = true });
		}


		[HttpPost("bulk-delete")]
		public async Task<IActionResult> DeleteReagent([FromBody] DeleteManyRequestDTO request) 
		{
            _logger.LogInformation($"Deleted reagent with ids in ReagentController");

            if (request.Ids == null || !request.Ids.Any())
                return BadRequest("No ids provided.");

            await _reagentService.DeleteAsync(request.Ids);
			return Ok(new { succes = true });
		}


		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateReagent([FromBody] ReagentUpdateDTO reagentDTO) 
		{
            _logger.LogInformation($"Updated notification with id = {reagentDTO.Id} in controller");
            
			var updatedValue = await _reagentService.UpdateAsync(reagentDTO);
            return Ok(updatedValue);
        }


        [HttpGet("stock-distribution")]
        public async Task<IActionResult> GetStockDistribution()
        {
            var report = await _reagentService.GetStockReportAsync();
            return Ok(report);
        }


        [HttpGet("expiring")]
        public async Task<ActionResult<List<ReagentExpirationDTO>>> GetExpiring()
			=> Ok(await _reagentService.GetExpiringReagentsReportAsync());


        [HttpGet("low-stock")]
        public async Task<ActionResult<List<ReagentLowStockDTO>>> GetLowStock([FromQuery] LowStockFilterDTO filter)
			=> Ok(await _reagentService.GetLowStockReportAsync(filter.CategoryId, filter.CriticalPercent, filter.ExcludeExpired));


        [HttpGet("forecast")]
        public async Task<IActionResult> GetReport()
			=> Ok(await _reagentForecastService.GetForecastAsync(3)); // пароговый мультипликатор
    }
}
