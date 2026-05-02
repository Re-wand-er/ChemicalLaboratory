using static ChemicalLaboratory.Infrastructure.Persistence.VisualTimeFormater;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        // Переопределен чтобы получать Категории
        public override async Task<IEnumerable<Notification>> GetAllAsync(bool includeInactive = false)
        {
            IQueryable<Notification> query = _dbSet;

            if (includeInactive)
                query = query.IgnoreQueryFilters();

            return await query.Include(n => n.User).Include(n => n.Reagent).ToListAsync();
        }

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

    }
}
