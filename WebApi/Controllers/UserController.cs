using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Application.UseCases.Services;
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
		[HttpGet("{id:int}")] public async Task<IActionResult> GetAllUsers(int id) => Ok(await _userService.GetByIdAsync(id));

		[HttpPost]
		public async Task<IActionResult> AddUser([FromBody] UserCreateDTO userDTO)
		{
            _logger.LogInformation("Creating user in controller");

            await _userService.AddAsync(userDTO);
            return Ok(new { success = true });
        }

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
            _logger.LogInformation($"Deleted user with id = {id} in controller");

            await _userService.DeleteAsync(id);
            return Ok(new { succes = true });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDTO)
        {
            _logger.LogInformation($"Updated user with id = {userDTO.Id} in controller");

            await _userService.UpdateAsync(userDTO);
            return Ok(new { success = true });
        }
    }
}
