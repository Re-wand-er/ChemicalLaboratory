using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentRepository : IBaseRepository<Reagent>
    {
        Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync();

        Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs);
        Task<ReagentStockReportDTO> GetStockDistributionReportAsync();
        Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync();
        Task<List<ReagentLowStockDTO>> GetLowStockReagentsAsync();
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
