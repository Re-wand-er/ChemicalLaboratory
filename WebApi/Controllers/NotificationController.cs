using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.DTOs;
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationDTO notificationDTO) 
        {
            _logger.LogInformation($"Updated notification with id = {notificationDTO.Id} in controller");

            var updatedValue = await _notificationService.UpdateAsync(notificationDTO);
            return Ok(updatedValue);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            return Ok(await _notificationService.GetUnreadCountAsync(userId));
        }


        [HttpGet("load")]
        public async Task<IActionResult> Get()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            return Ok(await _notificationService.GetNotificationsAsync(userId)); 
        }
        

        [HttpPost("read-all")]
        public async Task<IActionResult> ReadAll()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);

            await _notificationService.MarkAllAsReadAsync(userId);
            return NoContent();
        }


        [HttpPost("{id:int}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id); 
            return NoContent();
        }


        [HttpPost("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.SoftDeleteAsync(id); 
            return NoContent();
        }

    }
}
