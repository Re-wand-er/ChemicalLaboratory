using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Enums;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentRepository : IBaseRepository<Reagent>
    {
        // Basic
        //public Task<IEnumerable<Reagent>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync();
        Task<List<ReagentReportDTO>> GetReagentReportAsync(int? categoryId);
        Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs);


        
        // Smart
        Task<ReagentStockReportDTO> GetStockDistributionReportAsync();
        Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync(
            ExpirationStatus status,
            int? categoryId,
            int daysAhead = 90,
            bool onlyWithStock = true);
        
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
