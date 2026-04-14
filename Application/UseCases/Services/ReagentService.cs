using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;

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
            if (reagent == null)
            {
                _logger.LogWarning($"Reagent with id={id} not found");
                return null;
            }

            return reagent.Adapt<ReagentDTO>();
        }

        public async Task<IEnumerable<ListItemDTO?>> GetAllIdNameAsync()
            => await _unitOfWork.Reagents.GetAllIdNameAsync();

        public async Task AddAsync(ReagentDTO dto)
        {
            _logger.LogInformation($"Creating reagent with Name={dto.Name} with CurrentQuantity={dto.CurrentQuantity}");
            var reagent = dto.Adapt<Reagent>();

            await _unitOfWork.Reagents.AddAsync(reagent);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            _logger.LogInformation($"Deleted reagent with ids in ReagentService");

            await _unitOfWork.Reagents.DeleteManyAsync(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ReagentDTO> UpdateAsync(ReagentDTO dto)
        {
            _logger.LogInformation($"Updated reagent with id: {dto.Id}");

            var existingReagent = await _unitOfWork.Reagents.GetByIdAsync(dto.Id);
            if (existingReagent == null) throw new Exception("Reagent not found");

            dto.Adapt(existingReagent);

            await _unitOfWork.SaveAsync();

            return existingReagent.Adapt<ReagentDTO>();
        }

        public async Task<ReagentStockReportDTO> GetStockReportAsync()
        {
            _logger.LogInformation("Запрос распределения запасов по категориям");
            return await _unitOfWork.Reagents.GetStockDistributionReportAsync();
        }

        public async Task<List<ReagentExpirationDTO>> GetExpiringReagentsReportAsync()
            => await _unitOfWork.Reagents.GetExpiringReagentsAsync();

        public async Task<List<ReagentLowStockDTO>> GetLowStockReportAsync()
            => await _unitOfWork.Reagents.GetLowStockReagentsAsync();



        /// ReagentOperations ////////////////////////////////////////////////////////////
        public async Task<List<ItemDTO>> GetTopUsageReportAsync(
            ReportPeriod period,
            int topCount = 10,
            bool ascending = false)
        {
            try
            {
                _logger.LogInformation("Запрос отчета по потреблению за {Period}", period);

                // Ограничиваем topCount разумными пределами
                topCount = Math.Clamp(topCount, 1, 15);

                return await _unitOfWork.ReagentOperations.GetTopConsumingReagentsAsync(period, topCount, ascending);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при формировании отчета по реагентам");
                throw;
            }
        }
        public async Task<ReagentUsageTrendDTO> GetUsageTrendReportAsync(ReportPeriod period, ReportPeriod step)
        {
            _logger.LogInformation("Запрос тренда потребления: Период {Period}, Шаг {Step}", period, step);
            return await _unitOfWork.ReagentOperations.GetUsageTrendReportAsync(period, step);
        }

        public async Task<List<ItemDTO>> GetOperationsCountReportAsync(OperationsGroupBy groupBy, ReportPeriod period)
            => await _unitOfWork.ReagentOperations.GetOperationsCountReportAsync(groupBy, period);

        public async Task<List<ItemDTO>> GetAverageOperationSizeAsync(ReportPeriod period)
            => await _unitOfWork.ReagentOperations.GetAverageOperationSizeAsync(period);

        public async Task<List<ReagentTurnoverDTO>> GetTurnoverReportAsync(ReportPeriod period)
            => await _unitOfWork.ReagentOperations.GetTurnoverReportAsync(period);

    }
}
