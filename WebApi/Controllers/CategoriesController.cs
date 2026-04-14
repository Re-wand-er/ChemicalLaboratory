using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.WebApi.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoriesController : ControllerBase
    {
        private readonly IReagentCategoryRepository _reagentCategoryRepository;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IReagentCategoryRepository reagentCategoryRepository, ILogger<CategoriesController> logger)
        {
            _reagentCategoryRepository = reagentCategoryRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategories() => Ok(await _reagentCategoryRepository.GetAllAsync());
        

        [HttpGet("name")]
        public async Task<IActionResult> GetCategoriesName() => Ok(await _reagentCategoryRepository.GetAllIdNameAsync());


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO dto)
        {
            _logger.LogInformation("Creating Reagent in controller");

            var reagent = dto.Adapt<ReagentCategory>();

            await _reagentCategoryRepository.AddAsync(reagent);
            await _reagentCategoryRepository.SaveChangesAsync();

            return Ok(new { succes = true });
        }


        [HttpPost("bulk-delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteManyRequestDTO request)
        {
            _logger.LogInformation($"Deleted reagent with ids in ReagentController");

            if (request.Ids == null || !request.Ids.Any())
                return BadRequest("No ids provided.");

            await _reagentCategoryRepository.DeleteManyAsync(request.Ids);
            return Ok(new { succes = true });
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryDTO dto)
        {
            _logger.LogInformation($"Updated reagent with id: {id}");

            var existingCategory = await _reagentCategoryRepository.GetByIdAsync(id);
            if (existingCategory == null) throw new Exception("Reagent not found");

            dto.Adapt(existingCategory);

            await _reagentCategoryRepository.SaveChangesAsync();

            return Ok(existingCategory.Adapt<ReagentCategory>());
        }
    }
}
