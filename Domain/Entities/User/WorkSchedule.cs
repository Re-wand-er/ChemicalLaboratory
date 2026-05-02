using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Domain.Entities
{
    public class WorkSchedule : IEntity
    {
        public int Id { get; set; }
        public string? WorkShift { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
