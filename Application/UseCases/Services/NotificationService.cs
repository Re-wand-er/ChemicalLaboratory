using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using Mapster;
using ChemicalLaboratory.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

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


        public async Task<int> GetUnreadCountAsync(int userId)
            => await _unitOfWork.Notifications.GetUnreadCountAsync(userId);
        

        public async Task<List<NotificationSideBarDTO>> GetNotificationsAsync(int userId)
        {
            try
            {
                return await _unitOfWork.Notifications.GetUserNotificationsAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении уведомлений для пользователя {UserId}", userId);
                throw;
            }
        }


        public async Task MarkAllAsReadAsync(int userId)
        {
            try
            {
                await _unitOfWork.Notifications.MarkAllAsReadAsync(userId);
                _logger.LogInformation("Пользователь {UserId} пометил все уведомления как прочитанные", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при массовом прочтении уведомлений для {UserId}", userId);
                throw;
            }
        }


        public async Task MarkAsReadAsync(int id)
        {
            try
            {
                await _unitOfWork.Notifications.MarkAsReadAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пометке уведомления {Id} как прочитанного", id);
                throw;
            }
        }

        public async Task SoftDeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.Notifications.SoftDeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении уведомления {Id}", id);
                throw;
            }
        }

    }
}
