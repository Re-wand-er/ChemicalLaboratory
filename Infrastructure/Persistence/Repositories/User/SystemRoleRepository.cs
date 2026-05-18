using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class SystemRoleRepository: BaseRepository<SystemRole>, ISystemRoleRepository
    {
        public SystemRoleRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }

}
