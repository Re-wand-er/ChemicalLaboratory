using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class ExperimentRepository : IExperimentRepository
    {
        private readonly DataBaseContext _context;
        public ExperimentRepository(DataBaseContext context) => _context = context;

        public Task<ExperimentDTO> AddAsync(ExperimentDTO entity) { throw new NotImplementedException(); }
        public Task<ExperimentDTO> AddRangeAsync(ExperimentDTO entity) { throw new NotImplementedException(); }
        public Task<bool> UpdateAsync(ExperimentDTO entity) { throw new NotImplementedException(); }
        public async Task DeleteAsync(int id) 
        {
            Experiment? experiment = await _context.Experiments.FirstOrDefaultAsync(e => e.idExperiment == id);
            if (experiment != null)
            {
                _context.Experiments.Remove(experiment);
                // в идеале обработчик ошибок
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEquipmentAsync(int id) 
        {
            ExperimentEquipment? experimentEquipment = await _context.ExperimentEquipments.FirstOrDefaultAsync(e => e.idExpEq == id);
            if (experimentEquipment != null)
            {
                _context.ExperimentEquipments.Remove(experimentEquipment);
                // в идеале обработчик ошибок
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteReagentAsync(int id) 
        {
            ReagentExperiment? reagentEquipment = await _context.ReagentExperiments.FirstOrDefaultAsync(e => e.idReagExpetiment == id);
            if (reagentEquipment != null)
            {
                _context.ReagentExperiments.Remove(reagentEquipment);
                // в идеале обработчик ошибок
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ExperimentDTO>> GetAllAsync() 
        {
            return await _context.Experiments
                .AsNoTracking()
                .Select(e => new ExperimentDTO 
                {
                    idExperiment = e.idExperiment,
                    Name = e.Name,
                    Description = e.Description,
                    StartDate = e.StartDate ,
                    EndDate = e.EndDate ?? DateTime.MinValue,
                    Result = e.Result  ?? string.Empty,
                    Status = e.Status ?? string.Empty
                })
                .ToListAsync();
        }
    }
}
