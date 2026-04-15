using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentOperationRepository : BaseRepository<ReagentOperation>, IReagentOperationRepository
    {
        public ReagentOperationRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public async Task<ReagentUsageTrendDTO> GetUsageTrendReportAsync(ReportPeriod period, ReportPeriod step)
        {
            DateTime now = DateTime.UtcNow;
            DateTime startDate = ResolveByPeriod(period, () => now.AddDays(-7));

            // 2. ТОП-5 
            var top5Names = await _dbSet
                .Where(o => o.OperationDate >= startDate && o.Quantity < 0)
                .GroupBy(o => o.Reagent.Name)
                .OrderByDescending(g => g.Sum(x => Math.Abs(x.Quantity)))
                .Select(g => g.Key).Take(5).ToListAsync();

            var rawData = await _dbSet
                .Where(o => o.OperationDate >= startDate && o.Quantity < 0 && top5Names.Contains(o.Reagent.Name))
                .Select(o => new { Date = o.OperationDate, Name = o.Reagent.Name, Amount = Math.Abs(o.Quantity) })
                .ToListAsync();

            // 3. Генерация сетки согласно ШАГУ (Step)
            var allTimePoints = new List<string>();
            DateTime tempDate = startDate;
            while (tempDate <= now)
            {
                string formatted = FormatDateByPeriod(tempDate, step);
                if (!allTimePoints.Contains(formatted)) allTimePoints.Add(formatted);

                tempDate = step switch
                {
                    ReportPeriod.Day => tempDate.AddDays(1),
                    ReportPeriod.Week => tempDate.AddDays(7),
                    ReportPeriod.Month => tempDate.AddMonths(1),
                    ReportPeriod.Quarter => tempDate.AddMonths(3),
                    _ => tempDate.AddDays(1)
                };
            }

            var groupedData = rawData
                .GroupBy(d => FormatDateByPeriod(d.Date, step))
                .ToDictionary(g => g.Key, g => g.ToList());

            // 4. Сборка финального JSON
            var chartData = allTimePoints.Select(timePoint => {
                var dict = new Dictionary<string, object> { { "name", timePoint } };
                foreach (var name in top5Names)
                {
                    dict[name] = groupedData.ContainsKey(timePoint)
                        ? groupedData[timePoint].Where(x => x.Name == name).Sum(x => x.Amount)
                        : 0;
                }
                return dict;
            }).ToList();

            return new ReagentUsageTrendDTO { TopReagentNames = top5Names, ChartData = chartData };
        }

        public async Task<List<ItemDTO>> GetTopConsumingReagentsAsync(ReportPeriod period, int topCount = 5, bool ascending = false)
        {
            DateTime startDate = ResolveByPeriod(period, () => DateTime.MinValue);

            var query = _dbSet
                .Where(o => o.OperationDate >= startDate && o.Quantity < 0) // Только расход
                .GroupBy(o => new { o.ReagentId, o.Reagent.Name })
                .Select(g => new ItemDTO
                {
                    Name = g.Key.Name,
                    Value = Math.Abs(g.Sum(o => o.Quantity)) // Берем по модулю
                });

            // Сортировка
            query = ascending
                ? query.OrderBy(x => x.Value)
                : query.OrderByDescending(x => x.Value);

            return await query.Take(topCount).ToListAsync();
        }

        public async Task<List<ItemDTO>> GetOperationsCountReportAsync(OperationsGroupBy groupBy, ReportPeriod period)
        {
            var startDate = ResolveByPeriod(period, () => DateTime.UtcNow.AddDays(-30));

            var query = _dbSet.Where(o => o.OperationDate >= startDate);

            if (groupBy == OperationsGroupBy.Type)
            {
                return await query
                    .GroupBy(o => o.OperationType.Name) // Предполагаем наличие навигационного свойства
                    .Select(g => new ItemDTO { Name = g.Key, Value = g.Count() })
                    .ToListAsync();
            }
            else
            {
                return await query
                    .GroupBy(o => o.User.LastName + " " + o.User.FirstName + " " + o.User.MiddleName)
                    .Select(g => new ItemDTO
                    {
                        Name = g.Key.Trim(), // Trim на случай, если MiddleName пустой
                        Value = g.Count()
                    })
                    .ToListAsync();
            }
        }

        public async Task<List<ItemDTO>> GetAverageOperationSizeAsync(ReportPeriod period)
        {
            DateTime startDate = ResolveByPeriod(period, () => DateTime.MinValue);

            return await _dbSet
                .Where(o => o.OperationDate >= startDate)
                // Группируем по названию типа операции
                .GroupBy(o => o.OperationType.Name)
                .Select(g => new ItemDTO
                {
                    Name = g.Key,
                    Value = g.Average(x => Math.Abs(x.Quantity))
                })
                .ToListAsync();
        }

        public async Task<List<ReagentTurnoverDTO>> GetTurnoverReportAsync(ReportPeriod period)
        {
            DateTime startDate = ResolveByPeriod(period, () => DateTime.MinValue);

            // 1. Получаем все реагенты
            var reagents = await _context.Reagents.Where(r => r.IsActive).ToListAsync();

            // 2. Получаем операции за период (для расхода) и ПОСЛЕ начала периода (для вычисления нач. остатка)
            var operations = await _dbSet
                .Where(o => o.OperationDate >= startDate)
                .ToListAsync();

            var report = reagents.Select(r =>
            {
                // Расход за период (отрицательные значения Quantity)
                var consumption = Math.Abs(operations
                    .Where(o => o.ReagentId == r.Id && o.Quantity < 0)
                    .Sum(o => o.Quantity));

                // Вычисляем остаток на начало периода: 
                // Тек. остаток - (Сумма всех изменений с даты X до сегодня)
                var changesSinceStart = operations.Where(o => o.ReagentId == r.Id).Sum(o => o.Quantity);
                var stockAtStart = r.CurrentQuantity - changesSinceStart;

                // Средний остаток (упрощенно)
                var avgStock = (stockAtStart + r.CurrentQuantity) / 2;
                if (avgStock < 0) avgStock = 0; // Защита от некорректных данных


                // Коэффициент оборачиваемости
                decimal ratio = avgStock > 0 ? consumption / avgStock : 0;

                return new ReagentTurnoverDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    TotalConsumption = consumption,
                    AverageStock = avgStock,
                    TurnoverRatio = Math.Round(ratio, 2)
                };
            }).OrderByDescending(x => x.TurnoverRatio).ToList();

            return report;
        }

        public async Task<int> GetOperationsTodayCountAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .CountAsync(o => o.OperationDate >= today);
        }

        public async Task<List<RecentOperationDTO>> GetRecentOperationsAsync(int count = 7)
        {
            var operations = await _dbSet
                .Include(o => o.User)
                .Include(o => o.Reagent)
                .Include(o => o.OperationType)
                .OrderByDescending(o => o.OperationDate)
                .Take(count)
                .ToListAsync();

            return operations.Select(o => new RecentOperationDTO
            {
                Id = o.Id,
                OperationDate = o.OperationDate,
                // Столбец 1: ФИО
                UserFullName = $"{o.User.LastName} {o.User.FirstName[0]}.{o.User.MiddleName[0]}.",
                // Столбец 2: Суть действия
                ActionDetails = $"{o.OperationType.Name.ToLower()} {Math.Abs(o.Quantity)}{o.Reagent.Unit} {o.Reagent.Name}",
                RelativeTime = GetRelativeTime(o.OperationDate)
            }).ToList();
        }

        public async Task<List<UserActivityDto>> GetTopActiveUsersAsync(int days = 1, int top = 5)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-(days - 1));

            return await _dbSet
                .Where(o => o.OperationDate >= startDate)
                .GroupBy(o => new {
                    o.UserId,
                    o.User.LastName,
                    o.User.FirstName,
                    o.User.MiddleName
                })
                .Select(g => new UserActivityDto
                {
                    Id = g.Key.UserId,
                    FullName = $"{g.Key.LastName} {g.Key.FirstName[0]}.{g.Key.MiddleName[0]}.",
                    OperationsCount = g.Count()
                })
                .OrderByDescending(x => x.OperationsCount)
                .Take(top)
                .ToListAsync();
        }




        /// Private ////////////////////////////////////////////////////////////////////
        private static DateTime ResolveByPeriod(ReportPeriod period, Func<DateTime> defaultAction)
        {
            return period switch
            {
                ReportPeriod.Day => DateTime.UtcNow.AddDays(-1),
                ReportPeriod.Week => DateTime.UtcNow.AddDays(-7),
                ReportPeriod.Month => DateTime.UtcNow.AddMonths(-1),
                ReportPeriod.TwoMonth => DateTime.UtcNow.AddMonths(-2),
                ReportPeriod.Quarter => DateTime.UtcNow.AddMonths(-3),
                ReportPeriod.HalfYear => DateTime.UtcNow.AddMonths(-6),
                ReportPeriod.Year => DateTime.UtcNow.AddYears(-1),
                _ => defaultAction()
            };
        }

        private string FormatDateByPeriod(DateTime date, ReportPeriod step) => step switch
        {
            ReportPeriod.Day => date.ToString("dd.MM"),
            ReportPeriod.Week => $"Нед {System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}",
            ReportPeriod.Month => date.ToString("MMM yyyy"),
            _ => date.ToString("dd.MM")
        };

        private string GetRelativeTime(DateTime dateTime)
        {
            var span = DateTime.UtcNow - dateTime;
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} мин. назад";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours} час. назад";
            return dateTime.ToString("dd.MM HH:mm");
        }
    }
}
