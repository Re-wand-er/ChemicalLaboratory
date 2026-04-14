using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using Mapster;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class NotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUnitOfWork unitOfWork, ILogger<NotificationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllAsync() 
        {
            _logger.LogInformation("Get all notifications");
            var notifications = await _unitOfWork.Notifications.GetAllAsync();

            return notifications.Adapt<IEnumerable<NotificationDTO>>();
        }

        public async Task<NotificationDTO?> GetByIdAsync(int id) 
        {
            _logger.LogInformation($"Get notification with id: {id}");
            var notification = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notification == null)
            {
                _logger.LogWarning($"Notification with id = {id} not found");
                return null;
            }

            return notification.Adapt<NotificationDTO>();
        }

        public async Task AddAsync(NotificationDTO dto) 
        {
            _logger.LogInformation($"Creating notification with ReagentId={dto.ReagentId} with NotificationType={dto.NotificationType}");
            var notification = dto.Adapt<Notification>();
               
            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id) 
        {
            _logger.LogInformation($"Deleted notification with id: {id}");

            await _unitOfWork.Notifications.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<NotificationDTO> UpdateAsync(NotificationDTO dto)
        {
            _logger.LogInformation($"Updated notification with id: {dto.Id}");

            var existingNotification = await _unitOfWork.Notifications.GetByIdAsync(dto.Id);
            if (existingNotification == null) throw new Exception("Notification not found");

            dto.Adapt(existingNotification);

            await _unitOfWork.SaveAsync();

            return existingNotification.Adapt<NotificationDTO>();
        }
    }
}
