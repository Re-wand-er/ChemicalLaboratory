namespace ChemicalLaboratory.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int IdWorkSchedule { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string? LastName { get; set; } 
        public string Email { get; set; } = null!;
        public string? Sex { get; set; }
        public string? SystemRole { get; set; }
        public string? JobPosition { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }

        public WorkSchedule WorkSchedule { get; set; } = null!;
        public ICollection<ReagentOperation> ReagentOperations { get; set; } = new List<ReagentOperation>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
