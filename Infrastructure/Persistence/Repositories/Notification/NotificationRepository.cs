using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Application.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public Task AddRangeAsync(IEnumerable<Notification> notifications)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead && n.DeletedAt == null);
        }

        public async Task<List<NotificationSideBarDTO>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _dbSet
                .Include(n => n.Reagent)
                .Where(n => n.UserId == userId && n.DeletedAt == null)
                .OrderBy(n => n.IsRead)
                .ThenByDescending(n => n.CreatedAt)
                .ToListAsync();

            return notifications.Select(n => new NotificationSideBarDTO
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                ReagentName = n.Reagent?.Name ?? "Общее",
                IsRead = n.IsRead,
                CreatedAt = FormatRelativeTime(n.CreatedAt) 
            }).ToList();
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var unread = await _dbSet
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            unread.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            await _dbSet
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true));
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _dbSet
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(n => n.DeletedAt, DateTime.UtcNow));
        }



        // //////
        private string FormatRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return "только что";

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes switch
                {
                    1 => "минуту назад",
                    2 or 3 or 4 => $"{timeSpan.Minutes} минуты назад",
                    _ => $"{timeSpan.Minutes} минут назад"
                };

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours switch
                {
                    1 => "час назад",
                    2 or 3 or 4 => $"{timeSpan.Hours} часа назад",
                    _ => $"{timeSpan.Hours} часов назад"
                };

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days switch
                {
                    1 => "вчера",
                    _ => $"{timeSpan.Days} дней назад"
                };

            return dateTime.ToString("dd.MM.yyyy");
        }

    }
}
