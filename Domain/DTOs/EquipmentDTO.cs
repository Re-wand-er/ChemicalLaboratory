using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Domain.DTOs
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? Description { get; set; }
        public string Kind { get; set; } = null!;
        public string? Status { get; set; }

    }
}
