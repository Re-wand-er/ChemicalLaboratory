using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Domain.Entities
{
    public class Notification : IEntity
    {
        public int Id { get; set; }
        public int? ReagentId { get; set; }
        public int? UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? FilePath { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsRead { get; set; }

        public Reagent? Reagent { get; set; } = null!;
        public User? User { get; set; } = null!;

        public static Notification Create(
            int userId, 
            Reagent? reagent, 
            string notificationType, 
            string title, 
            string message,
            string? filePath = null) 
        {
            return new Notification
            {
                UserId = userId,
                NotificationType = notificationType,
                Title = title,
                Message = message,
                FilePath = filePath,
                IsRead = false,
                Reagent = reagent,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null
            };
        }
    }
}
