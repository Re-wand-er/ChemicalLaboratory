using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class ReagentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ReagentService> _logger;

        public ReagentService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ILogger<ReagentService> logger)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IEnumerable<ReagentDTO>> GetAllAsync(bool includeInactive = false)
        {
            _logger.LogInformation($"Get all reagents");
            var reagents = await _unitOfWork.Reagents.GetAllAsync(includeInactive);
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
            var userId = _currentUserService.GetRequiredUserId();

            _logger.LogInformation($"Creating reagent with Name={dto.Name} with CurrentQuantity={dto.CurrentQuantity}");
            
            var reagent = dto.Adapt<Reagent>();
            await _unitOfWork.Reagents.AddAsync(reagent);

            var historyEntry = ReagentOperation.Create(userId, OperationTypeEnum.Receipt, reagent);
            await _unitOfWork.ReagentOperations.AddAsync(historyEntry);

            await _unitOfWork.SaveAsync();
        }

        //public async Task DeleteAsync(IEnumerable<int> ids)
        //{
        //    var userId = _currentUserService.GetRequiredUserId();

        //    _logger.LogInformation($"Deleted reagent with ids in ReagentService");

        //    foreach (var id in ids)
        //    {
        //        var historyEntry = ReagentOperation.CreateForDeletion(userId, id, $"Удаление реагента с id: {id}");
        //        await _unitOfWork.ReagentOperations.AddAsync(historyEntry);
        //    }

        //    await _unitOfWork.Reagents.DeleteManyAsync(ids);

        //    await _unitOfWork.SaveAsync();
        //}

        public async Task DeleteAsync(IEnumerable<int> ids, bool hardDelete = false)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            if (hardDelete)
            {
                // Проверяем, имеет ли право на полное удаление
                //if (userRole != SystemRoleEnum.SuperAdmin)
                //    throw new AccessDeniedException("Только Супер-администратор может удалять данные безвозвратно.");

                // Логика Hard Delete
                //await _unitOfWork.Reagents.DeleteHardManyAsync(ids);
            }
            else
            {
                foreach (var id in ids)
                {
                    var history = ReagentOperation.CreateForDeletion(userId, id, "Мягкое удаление (архивация)");
                    await _unitOfWork.ReagentOperations.AddAsync(history);
                }

                await _unitOfWork.Reagents.SoftDeleteManyAsync(ids);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<ReagentDTO> UpdateAsync(ReagentUpdateDTO dto)
        {
            var userId = _currentUserService.GetRequiredUserId();

            _logger.LogInformation($"Updated reagent with id: {dto.Id}");

            var existingReagent = await _unitOfWork.Reagents.GetByIdAsync(dto.Id);
            if (existingReagent == null) 
                throw new KeyNotFoundException("Reagent not found");

            dto.Adapt(existingReagent);

            var historyEntry = ReagentOperation.Create(userId, OperationTypeEnum.Update, existingReagent, "Редактирование параметров реактива");
            
            await _unitOfWork.ReagentOperations.AddAsync(historyEntry);
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

        public async Task<List<ReagentLowStockDTO>> GetLowStockReportAsync(int? categoryId, decimal percent, bool expired)
            => await _unitOfWork.Reagents.GetLowStockReagentsAsync(categoryId, percent, expired);


        public async Task<DashboardDTO> GetMainDashboardKpiAsync()
        {
            return new DashboardDTO
            {
                ActiveReagentsCount = await _unitOfWork.Reagents.GetActiveCountAsync(),
                LowStockPercentage = await _unitOfWork.Reagents.GetLowStockPercentageAsync(),
                ExpiredPercentage = await _unitOfWork.Reagents.GetExpiredPercentageAsync(),
                ExpiringSoonPercentage = await _unitOfWork.Reagents.GetExpiringSoonPercentageAsync(),
                OperationsTodayCount = await _unitOfWork.ReagentOperations.GetOperationsTodayCountAsync(),
                IlliquidPercentage = await _unitOfWork.Reagents.GetIlliquidPercentageAsync(),
                DsiDays = await _unitOfWork.Reagents.GetDsiDaysAsync(),
            };
        }


        public async Task<List<ReagentExpirationDTO>> GetExpirationCalendarAsync()
            => await _unitOfWork.Reagents.GetUpcomingExpirationsAsync(5);


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

        public async Task<List<RecentOperationDTO>> GetRecentActivityAsync()
            => await _unitOfWork.ReagentOperations.GetRecentOperationsAsync(7);
        public async Task<List<UserActivityDto>> GetUserActivityTopAsync(int days)
            => await _unitOfWork.ReagentOperations.GetTopActiveUsersAsync(days);
    }
}
