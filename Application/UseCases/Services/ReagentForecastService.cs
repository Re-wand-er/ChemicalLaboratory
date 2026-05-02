using ChemicalLaboratory.Application.UseCases.DTOs.Filters;
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

        //public async Task<List<ReagentPredictionReportDTO>> GetForecastAsync(int multiplier = 3)
        //{
        //    var rawData = await _reagentRepository.GetConsumptionHistoryAsync();
        //    var results = new List<ReagentPredictionReportDTO>();
        //    DateOnly? deadline = null;
        //    int leadTime = 30;
        //    int minDate = 0;

        //    foreach (var item in rawData)
        //    {
        //        decimal predictedDailyConsumption = item.AvgDailyConsumption;

        //        // Расчеты
        //        int daysToZero = predictedDailyConsumption > 0
        //            ? (int)(item.CurrentQuantity / predictedDailyConsumption) : 999;

        //        decimal maxQuantity = item.MinQuantity * multiplier; // Пример Max
        //        decimal recOrder = 0;

        //        minDate = Math.Min(daysToZero, item.DaysToExpiry ?? 999);

        //        if (minDate <= leadTime + 2) // Запас в 2 дня
        //        {
        //            recOrder = maxQuantity - item.CurrentQuantity;
        //            deadline = DateOnly.FromDateTime(DateTime.Now).AddDays(Math.Max(0, minDate - leadTime));
        //        }

        //        results.Add(new ReagentPredictionReportDTO
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            CurrentQuantity = item.CurrentQuantity,
        //            MinQuantity = item.MinQuantity,
        //            Unit = item.Unit,
        //            AvgConsumption = predictedDailyConsumption,
        //            DaysToZero = daysToZero,
        //            DaysToExpiry = item.DaysToExpiry ?? 999,
        //            RecommendedOrder = recOrder > 0 ? recOrder : 0,
        //            OrderDeadline = deadline
        //        });
        //    }
        //    return results;
        //}

        public async Task<List<ReagentPredictionReportDTO>> GetForecastAsync(ForecastFilterDTO filter)
        {
            var rawData = await _reagentRepository.GetConsumptionHistoryAsync(filter.ForecastDays);

            var results = new List<ReagentPredictionReportDTO>();

            foreach (var item in rawData)
            {
                decimal avg = item.AvgDailyConsumption;

                int daysToZero = avg > 0
                    ? (int)(item.CurrentQuantity / avg)
                    : 999;

                int daysToExpiry = item.DaysToExpiry ?? 999;

                int minDate = Math.Min(daysToZero, daysToExpiry);

                decimal recommended = 0;

                bool needsOrder = minDate <= 30;

                if (needsOrder)
                {
                    decimal maxQty = item.MinQuantity * filter.Multiplier;
                    recommended = maxQty - item.CurrentQuantity;
                }

                var dto = new ReagentPredictionReportDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    CurrentQuantity = item.CurrentQuantity,
                    MinQuantity = item.MinQuantity,
                    Unit = item.Unit,
                    AvgConsumption = avg,
                    DaysToZero = daysToZero,
                    DaysToExpiry = daysToExpiry,
                    RecommendedOrder = recommended > 0 ? recommended : 0
                };

                results.Add(dto);
            }

            // 🔥 фильтры после расчета (ВАЖНО)
            if (filter.MaxDaysToZero.HasValue)
                results = results
                    .Where(x => x.DaysToZero <= filter.MaxDaysToZero)
                    .ToList();

            if (filter.CriticalOnly)
                results = results
                    .Where(x => x.DaysToZero < 30 || x.DaysToExpiry < 30)
                    .ToList();

            if (filter.OnlyReorderNeeded)
                results = results
                    .Where(x => x.RecommendedOrder > 0)
                    .ToList();


            return results;
        }
    }
}
