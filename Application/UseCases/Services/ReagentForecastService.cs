using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class ReagentForecastService
    {
        private readonly IReagentRepository _reagentRepository;

        public ReagentForecastService(IReagentRepository reagentRepository)
        {
            _reagentRepository = reagentRepository;
        }

        public async Task<List<ReagentReportDTO>> GetForecastAsync(int multiplier = 3)
        {
            var rawData = await _reagentRepository.GetConsumptionHistoryAsync();
            var results = new List<ReagentReportDTO>();
            DateOnly? deadline = null;
            int leadTime = 30;
            int minDate = 0;

            foreach (var item in rawData)
            {
                decimal predictedDailyConsumption = item.AvgDailyConsumption;

                // Расчеты
                int daysToZero = predictedDailyConsumption > 0
                    ? (int)(item.CurrentQuantity / predictedDailyConsumption) : 999;

                decimal maxQuantity = item.MinQuantity * multiplier; // Пример Max
                decimal recOrder = 0;

                minDate = Math.Min(daysToZero, item.DaysToExpiry ?? 999);

                if (minDate <= leadTime + 2) // Запас в 2 дня
                {
                    recOrder = maxQuantity - item.CurrentQuantity;
                    deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(Math.Max(0, minDate - leadTime));
                }

                results.Add(new ReagentReportDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    CurrentQuantity = item.CurrentQuantity,
                    MinQuantity = item.MinQuantity,
                    Unit = item.Unit,
                    AvgConsumption = predictedDailyConsumption,
                    DaysToZero = daysToZero,
                    DaysToExpiry = item.DaysToExpiry ?? 999,
                    RecommendedOrder = recOrder > 0 ? recOrder : 0,
                    OrderDeadline = deadline
                });
            }
            return results;
        }
    }
}
