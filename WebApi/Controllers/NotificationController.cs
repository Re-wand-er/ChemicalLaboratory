using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.WebApi.Models;
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

        public NotificationController(NotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
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
            => Ok(await _notificationService.GetUnreadCountAsync());
       


        [HttpGet("load")]
        public async Task<IActionResult> Get()
            => Ok(await _notificationService.GetNotificationsAsync()); 
        

        [HttpPost("read-all")]
        public async Task<IActionResult> ReadAll()
        {
            await _notificationService.MarkAllAsReadAsync();
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

        // [HttpPost("bulk-delete")]
        // public async Task<IActionResult> DeleteCategory([FromBody] DeleteManyRequestDTO request)
        // {
        //     _logger.LogInformation($"Deleted reagent with ids in ReagentController");

        //     if (request.Ids == null || !request.Ids.Any())
        //         return BadRequest("No ids provided.");

        //     await _notificationService.DeleteAsync(request.Ids);
        //     return Ok(new { succes = true });
        // }

    }
}
