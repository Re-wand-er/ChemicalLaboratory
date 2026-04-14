using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class WorkScheduleRepository : BaseRepository<WorkSchedule>, IWorkSchedule
    {
        public WorkScheduleRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
