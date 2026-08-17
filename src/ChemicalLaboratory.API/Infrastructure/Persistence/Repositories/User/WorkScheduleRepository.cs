using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class WorkScheduleRepository : BaseRepository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
