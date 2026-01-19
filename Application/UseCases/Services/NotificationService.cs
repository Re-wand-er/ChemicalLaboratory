using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
            //_notificationRepository.Update();
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllAsync() 
        {
            _logger.LogInformation("Get all notifications");
            var notifications = await _notificationRepository.GetAllAsync();
            return notifications
                    .Select(n => new NotificationDTO(n.Id, n.ReagentId, n.NotificationType, n.Message, n.CreatedAt, n.IsRead))
                    .ToList();
        }

        public async Task<NotificationDTO?> GetByIdAsync(int id) 
        {
            _logger.LogInformation($"Get notification with id: {id}");
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null)
            {
                _logger.LogWarning($"Notification with id = {id} not found");
                return null;
            }

            return new NotificationDTO(notification.Id, notification.ReagentId, notification.NotificationType, notification.Message, notification.CreatedAt, notification.IsRead);
        }

        public async Task AddAsync(NotificationDTO notificationDTO) 
        {
            _logger.LogInformation($"Creating notification with ReagentId={notificationDTO.ReagentId} with NotificationType={notificationDTO.NotificationType}");
            var notification = new Notification()
            { 
                Id = notificationDTO.Id,
                ReagentId = notificationDTO.ReagentId,
                NotificationType = notificationDTO.NotificationType,
                Message = notificationDTO.Message,
                CreatedAt = notificationDTO.CreatedAt,
                IsRead = notificationDTO.IsRead
            };

            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            _logger.LogInformation($"Deleted notification with id: {id}");
            await _notificationRepository.DeleteAsync(id);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task Update(NotificationDTO notificationDTO)
        {
            _logger.LogInformation($"Updated notification with id: {notificationDTO.Id}");
            var notification = new Notification()
            {
                Id = notificationDTO.Id,
                ReagentId = notificationDTO.ReagentId,
                NotificationType = notificationDTO.NotificationType,
                Message = notificationDTO.Message,
                CreatedAt = notificationDTO.CreatedAt,
                IsRead = notificationDTO.IsRead
            };

            _notificationRepository.Update(notification);
            await _notificationRepository.SaveChangesAsync();
        }
    }
}
