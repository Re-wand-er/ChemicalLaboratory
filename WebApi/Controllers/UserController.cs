using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
	{
		private readonly UserService _userService;
		private readonly ILogger<UserController> _logger;

		public UserController(UserService userService, ILogger<UserController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		[HttpGet] public async Task<IActionResult> GetAllUsers() => Ok(await _userService.GetAllAsync());
        [HttpGet("id")] public async Task<IActionResult> GetIdUsers()
            => Ok(await _userService.GetByIdAsync());


        [HttpGet("name")]
        public async Task<IActionResult> GetCategoriesName() => Ok(await _userService.GetAllIdNameAsync());


        [HttpPost]
		public async Task<IActionResult> AddUser([FromBody] UserCreateDTO userDTO)
		{
            _logger.LogInformation("Creating user in controller");

            await _userService.AddAsync(userDTO);
            return Ok(new { success = true });
        }

		[HttpPost("bulk-delete")]
		public async Task<IActionResult> DeleteUser([FromBody] DeleteManyRequestDTO request)
		{
            _logger.LogInformation($"Deleted user with ids UserController");

            if (request.Ids == null || !request.Ids.Any())
                return BadRequest("No ids provided.");

            await _userService.DeleteAsync(request.Ids);
            return Ok(new { succes = true });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDTO)
        {
            _logger.LogInformation($"Updated user with id = {userDTO.Id} in controller");

            var updatedValue = await _userService.UpdateAsync(userDTO);
            return Ok(updatedValue);
        }
    }
}
