using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentRepository : IBaseRepository<Reagent>
    {
        // Basic
        public Task<IEnumerable<Reagent>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync();
        Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs);

        // SoftDelete
        Task SoftDeleteAsync(IEnumerable<int> ids);
        Task RestoreAsync(IEnumerable<int> ids);


        
        // Smart
        Task<ReagentStockReportDTO> GetStockDistributionReportAsync();
        Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync();
        Task<List<ReagentLowStockDTO>> GetLowStockReagentsAsync(int? categoryId, decimal percent, bool expired);
        Task<List<ReagentPredictionDTO>> GetConsumptionHistoryAsync(int daysLookback = 90);
        Task<int> GetActiveCountAsync();
        Task<double> GetLowStockPercentageAsync();
        Task<double> GetExpiringSoonPercentageAsync();
        Task<double> GetExpiredPercentageAsync();
        Task<double> GetIlliquidPercentageAsync(int days = 90);
        Task<double> GetDsiDaysAsync(int days = 90);
        Task<List<ReagentExpirationDTO>> GetUpcomingExpirationsAsync(int count = 5);
    }
}
