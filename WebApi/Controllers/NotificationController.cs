using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly NotificationService _notificationService;

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
            await _notificationService.AddAsync(notificationDTO);
            return Ok(new { success = true });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(int id) 
        {
            await _notificationService.DeleteAsync(id);
            return Ok(new { succes = true });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationDTO notificationDTO) 
        {
            await _notificationService.Update(notificationDTO);
            return Ok(new { success = true });
        }
    }
}
