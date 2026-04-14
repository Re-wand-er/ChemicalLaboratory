using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.WebApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/supplier")]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(SupplierService supplierService, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }

        [HttpGet] public async Task<IActionResult> GetAllSuppliers() => Ok(await _supplierService.GetAllAsync());

        [HttpGet("{id:int}")] public async Task<IActionResult> GetSupplierById(int id) => Ok(await _supplierService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierWithoutIdDTO dto)
        {
            _logger.LogInformation("Creating supplier in controller");

            await _supplierService.AddAsync(dto.Adapt<SupplierDTO>());
            return Ok(new { succes = true });
        }

        [HttpPost("bulk-delete")]
        public async Task<IActionResult> DeleteSupplier([FromBody] DeleteManyRequestDTO request) 
        {
            _logger.LogInformation($"Deleted supplier with ids in SupplierController");

            if (request.Ids == null || !request.Ids.Any())
                return BadRequest("No ids provided.");

            await _supplierService.DeleteAsync(request.Ids);
            return Ok(new { succes = true });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierDTO dto)
        {
            _logger.LogInformation($"Updated supplier with id = {dto.Id} in controller");

            var updatedValue = await _supplierService.UpdateAsync(dto);
            return Ok(updatedValue);
        }
    }
}
