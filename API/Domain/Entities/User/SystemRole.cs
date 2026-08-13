using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Domain.Entities
{
    public class SystemRole : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public ICollection<User> Users { get; set; } = [];
    }
}
