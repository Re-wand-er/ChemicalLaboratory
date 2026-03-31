using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger, NotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet] public async Task<IActionResult> GetAllNotifications() => Ok(await _notificationService.GetAllAsync()); 
        [HttpGet("{id:int}")] public async Task<IActionResult> GetNotificationById(int id) => Ok(await _notificationService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] NotificationDTO notificationDTO)
        {
            _logger.LogInformation("Creating notification in controller");

            await _notificationService.AddAsync(notificationDTO);
            return Ok(new { success = true });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(int id) 
        {
            _logger.LogInformation($"Deleted notification with id = {id} in controller");

            await _notificationService.DeleteAsync(id);
            return Ok(new { succes = true });
        }


        // Возможен также такой вариант
        // PUT /api/users/5
        // Body: UserUpdateDTO
        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody] UserUpdateDTO dto)

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationDTO notificationDTO) 
        {
            _logger.LogInformation($"Updated notification with id = {notificationDTO.Id} in controller");

            await _notificationService.UpdateAsync(notificationDTO);
            return Ok(new { success = true });
        }
    }
}
