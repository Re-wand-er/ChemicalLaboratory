using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDTO supplierDTO)
        {
            _logger.LogInformation("Creating supplier in controller");

            await _supplierService.AddAsync(supplierDTO);
            return Ok(new { succes = true });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSupplier(int id) 
        {
            _logger.LogInformation($"Deleted supplier with id = {id} in controller");

            await _supplierService.DeleteAsync(id);
            return Ok(new { succes = true });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierDTO supplierDTO)
        {
            _logger.LogInformation($"Updated supplier with id = {supplierDTO.Id} in controller");

            await _supplierService.UpdateAsync(supplierDTO);
            return Ok(new { success = true });
        }
    }
}
