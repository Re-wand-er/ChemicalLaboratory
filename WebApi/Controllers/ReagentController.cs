using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ChemicalLaboratory.Application.UseCases.DTOs.Filters;

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
			=> Ok(await _reagentService.GetAllAsync(includeInactive));


		[HttpGet("{id:int}")] 
		public async Task<IActionResult> GetReagentById(int id) 
			=> Ok(await _reagentService.GetByIdAsync(id));

        [HttpGet("name")]
        public async Task<IActionResult> GetCategoriesName() 
			=> Ok(await _reagentService.GetAllIdNameAsync());


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

        [HttpPost("bulk-restore")]
        public async Task<IActionResult> RestoreReagent([FromBody] DeleteManyRequestDTO request)
        {
            _logger.LogInformation($"Restored reagent with ids in ReagentController");

            if (request.Ids == null || !request.Ids.Any())
                return BadRequest("No ids provided.");

            await _reagentService.RestoreAsync(request.Ids);
            return Ok(new { succes = true });
        }

        [HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateReagent([FromBody] ReagentUpdateDTO reagentDTO) 
		{
            _logger.LogInformation($"Updated notification with id = {reagentDTO.Id} in controller");
            
			var updatedValue = await _reagentService.UpdateAsync(reagentDTO);
            return Ok(updatedValue);
        }

		[HttpPut("batch")]
		public async Task<IActionResult> UpdateReagentsBatch(
		    [FromBody] List<ReagentUpdateDTO> reagents)
		{
		    if (reagents == null || !reagents.Any())
		        return BadRequest("Empty update list");
		
		    _logger.LogInformation($"Batch update reagents count = {reagents.Count}");
		
		    var result = await _reagentService.UpdateBatchAsync(reagents);
		
		    return Ok(result);
		}


		[HttpPost("income-by-qr")]
    	public async Task<IActionResult> UploadQrImages([FromForm] List<IFormFile> files)
    	{
    	    if (files == null || !files.Any())
    	        return BadRequest("Файлы изображений не переданы.");
	
    	    var streams = new List<Stream>();
    	    try
    	    {
    	        foreach (var file in files)
    	        {
    	            var memoryStream = new MemoryStream();
    	            await file.CopyToAsync(memoryStream);
					
					memoryStream.Position = 0; 
    	            streams.Add(memoryStream);
    	        }
	
    	        // Запускаем наш процесс распознавания и пакетного обновления
    	        var result = await _reagentService.ProcessIncomeFromQrImagesAsync(streams);
    	        return Ok(result);
    	    }
    	    finally
    	    {
    	        // Обязательно освобождаем потоки из памяти
    	        foreach (var stream in streams)
    	        {
    	            await stream.DisposeAsync();
    	        }
    	    }
    	}


        [HttpGet("stock-distribution")]
        public async Task<IActionResult> GetStockDistribution()
        {
            var report = await _reagentService.GetStockReportAsync();
            return Ok(report);
        }


        [HttpGet("expiring")]
        public async Task<ActionResult<List<ReagentExpirationDTO>>> GetExpiring([FromQuery] ExpirationFilterDTO filter)
			=> Ok(await _reagentService.GetExpiringReagentsReportAsync(filter));


        [HttpGet("low-stock")]
        public async Task<ActionResult<List<ReagentLowStockDTO>>> GetLowStock([FromQuery] LowStockFilterDTO filter)
			=> Ok(await _reagentService.GetLowStockReportAsync(filter.CategoryId, filter.CriticalPercent, filter.ExcludeExpired));


        [HttpGet("forecast")]
        public async Task<ActionResult<List<ReagentPredictionReportDTO>>> GetForecast([FromQuery] ForecastFilterDTO filter)
            => Ok(await _reagentForecastService.GetForecastAsync(filter));

        [HttpGet("report")]
        public async Task<ActionResult<List<ReagentReportDTO>>> GetReagentReport([FromQuery] int? categoryId)
			=> Ok(await _reagentService.GetReagentReportAsync(categoryId));


		[HttpPost("export-order-pdf")]
		public async Task<IActionResult> DownloadOrderPdf([FromBody] List<ReagentOrderInputDTO> orderItems)
		{
		    if (orderItems == null || !orderItems.Any())
		        return BadRequest("Список заказа пуст.");

		    string relativePath = await _reagentService.CreateOrderInvoiceFromInputsAsync(orderItems);

		    if (string.IsNullOrEmpty(relativePath))
		        return NotFound("Не удалось сформировать отчет. Реагенты не найдены.");

		    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));
		    if (!System.IO.File.Exists(fullPath))
		        return NotFound("Файл отчета отсутствует на сервере.");

		    var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
		    string fileDownloadName = $"Заявка_Закупка_{DateTime.Now:dd_MM_yyyy}.pdf";

		    return File(fileBytes, "application/pdf", fileDownloadName);
		}

    }
}
