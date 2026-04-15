using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentOperationRepository : IBaseRepository<ReagentOperation>
    {
        Task<List<ItemDTO>> GetTopConsumingReagentsAsync(ReportPeriod period, int topCount = 5, bool ascending = false);
        Task<List<ItemDTO>> GetOperationsCountReportAsync(OperationsGroupBy groupBy, ReportPeriod period);
        Task<ReagentUsageTrendDTO> GetUsageTrendReportAsync(ReportPeriod period, ReportPeriod step);
        Task<List<ItemDTO>> GetAverageOperationSizeAsync(ReportPeriod period);
        Task<List<ReagentTurnoverDTO>> GetTurnoverReportAsync(ReportPeriod period);
        Task<int> GetOperationsTodayCountAsync();
        Task<List<RecentOperationDTO>> GetRecentOperationsAsync(int count = 7);
        Task<List<UserActivityDto>> GetTopActiveUsersAsync(int days = 1, int top = 5);
    }
}
