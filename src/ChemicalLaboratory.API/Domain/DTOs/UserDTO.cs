using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.DTOs
{
    public record UserDTO
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
    }
}
