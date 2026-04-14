using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int? UserId { get; set; }
        public string NotificationType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public Reagent Reagent { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
