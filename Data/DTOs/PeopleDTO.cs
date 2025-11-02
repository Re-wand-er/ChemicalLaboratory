namespace EFCore.DTOs
{
    public record PeopleDTO
    {
        public int IdPeople { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public string? Email { get; set; }
        public string? JobPosition { get; set; }
        public string? SystemRole { get; set; }
        public string? Login { get; set; }

        public WorkScheduleDTO? WorkSchedule { get; set; }
    }
}
