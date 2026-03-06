using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using Mapster;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class ReagentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReagentService> _logger;

        public ReagentService(IUnitOfWork unitOfWork, ILogger<ReagentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<ReagentDTO>> GetAllAsync() 
        {
            _logger.LogInformation($"Get all reagents");
            var reagents = await _unitOfWork.Reagents.GetAllAsync();
            return reagents.Adapt<IEnumerable<ReagentDTO>>();
        }

        public async Task<ReagentDTO?> GetByIdAsync(int id) 
        {
            _logger.LogInformation($"Get reagent with id:{id}");

            var reagent = await _unitOfWork.Reagents.GetByIdAsync(id);
            if(reagent == null)
            {
                _logger.LogWarning($"Reagent with id={id} not found");
                return null;
            }

            return reagent.Adapt<ReagentDTO>();
        }

        public async Task AddAsync(ReagentDTO dto)
        {
            _logger.LogInformation($"Creating reagent with Name={dto.Name} with CurrentQuantity={dto.CurrentQuantity}");
            var reagent = dto.Adapt<Reagent>();

            await _unitOfWork.Reagents.AddAsync(reagent);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleted reagent with id: {id}");

            await _unitOfWork.Reagents.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ReagentDTO dto)
        {
            _logger.LogInformation($"Updated reagent with id: {dto.Id}");

            var reagent = dto.Adapt<Reagent>();

            _unitOfWork.Reagents.Update(reagent);
            await _unitOfWork.SaveAsync();
        }
    }
}
