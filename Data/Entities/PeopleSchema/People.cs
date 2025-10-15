namespace EFCore.Entities
{
    internal class People
    {
        public int idPeople { get; set; }
        public int? idExperiment { get; set; }
        public int idWorkSchedule { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;// uniq
        public string Sex { get; set; } = null!; // CHECK (Sex IN ('М', 'Ж'))
        public string? SystemRole { get; set; }
        public string? JobPosition { get; set; }
        public string Login { get; set; } = null!;
        public string? PasswordHash { get; set; }

        public WorkSchedule? WorkSchedule { get; set; }
    }
}
