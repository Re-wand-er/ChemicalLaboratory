using static ChemicalLaboratory.Infrastructure.Persistence.VisualTimeFormater; // Не должно быть здесь
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentRepository : BaseRepository<Reagent>, IReagentRepository
    {
        public ReagentRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        // Переопределен чтобы получать Категории
        public override async Task<IEnumerable<Reagent>> GetAllAsync(bool includeInactive = false)
        {
            IQueryable<Reagent> query = _dbSet;

            if (includeInactive)
                query = query.IgnoreQueryFilters();

            return await query.Include(r => r.Category).ToListAsync();
        }


        public async Task<List<ReagentReportDTO>> GetReagentReportAsync(int? categoryId)
        {
            var query = _context.Reagents
                .Where(r => r.IsActive);

            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            return await query
                .Select(r => new ReagentReportDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Category = r.Category.Name,
                    CurrentQuantity = r.CurrentQuantity,
                    MinQuantity = r.MinQuantity,
                    Unit = r.Unit
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync() 
            => await _dbSet
                .AsNoTracking()
                .Select(c => new ListItemDTO(c.Id, c.Name))
                .ToListAsync();


        public virtual Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs) 
            => throw new NotImplementedException();

        //public override async Task DeleteManyAsync(IEnumerable<int> ids)
        //{
        //    var entities = await _dbSet
        //        .Where(c => ids.Contains(c.Id))
        //        .ToListAsync();

        //    _dbSet.RemoveRange(entities);
        //    await _context.SaveChangesAsync();
        //}




        // Smart
        public async Task<ReagentStockReportDTO> GetStockDistributionReportAsync()
        {
            // 1. Получаем сгруппированные данные из БД (Категория + Реагент = Сумма)
            var rawData = await _dbSet
                .Where(r => r.IsActive)
                .GroupBy(r => new { CategoryName = r.Category.Name, ReagentName = r.Name })
                .Select(g => new
                {
                    Category = g.Key.CategoryName,
                    Reagent = g.Key.ReagentName,
                    Total = g.Sum(r => r.CurrentQuantity)
                })
                .ToListAsync();

            // 2. Формируем категории (внутренний круг), сортируем по убыванию значения
            var categories = rawData
                .GroupBy(d => d.Category)
                .Select(g => new ItemDTO
                {
                    Name = g.Key,
                    Value = g.Sum(x => x.Total)
                })
                .OrderBy(x => x.Value)
                .ToList();

            // 3. Формируем реагенты (внешний круг). 
            // ВАЖНО: они должны идти в том же порядке групп, что и категории!
            var reagents = new List<ItemDTO>();
            foreach (var cat in categories)
            {
                var reagentsInCat = rawData
                    .Where(d => d.Category == cat.Name)
                    .OrderBy(d => d.Total) // Внутри категории тоже сортируем для красоты
                    .Select(d => new ItemDTO { Name = d.Reagent, Value = d.Total });

                reagents.AddRange(reagentsInCat);
            }

            return new ReagentStockReportDTO
            {
                Categories = categories,
                Reagents = reagents
            };
        }


        //public async Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync()
        //{
        //    var today = DateTime.UtcNow.Date;
        //    var limitDate = today.AddDays(90);

        //    return await _dbSet
        //        .Where(r => r.IsActive && r.ExpirationDate != null && r.ExpirationDate <= limitDate)
        //        .OrderBy(r => r.ExpirationDate)
        //        .Select(r => new ReagentExpirationDTO
        //        {
        //            Id = r.Id,
        //            Name = r.Name,
        //            ExpirationDate = r.ExpirationDate,
        //            DaysRemaining = EF.Functions.DateDiffDay(today, r.ExpirationDate!.Value).ToString() // Срок годности может быть null
        //        })
        //        .ToListAsync();
        //}


        public async Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync(
            ExpirationStatus status, 
            int? categoryId,
            int daysAhead = 90,
            bool onlyWithStock = true)
        {
            var today = DateTime.UtcNow.Date;
            var limitDate = today.AddDays(daysAhead);

            var query = _dbSet
                .Where(r => r.IsActive && r.ExpirationDate != null);

            // Фильтр по категории
            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            // Исключить нулевой остаток
            if (onlyWithStock)
                query = query.Where(r => r.CurrentQuantity > 0);

            // Фильтр по статусу
            switch (status)
            {
                case ExpirationStatus.ExpiredOnly:
                    query = query.Where(r => r.ExpirationDate < today);
                    break;

                case ExpirationStatus.ExpiringSoon:
                    query = query.Where(r =>
                        r.ExpirationDate >= today &&
                        r.ExpirationDate <= limitDate);
                    break;

                case ExpirationStatus.All:
                    query = query.Where(r =>
                        r.ExpirationDate <= limitDate);
                    break;
            }

            return await query
                .OrderBy(r => r.ExpirationDate)
                .Select(r => new ReagentExpirationDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Category = r.Category.Name,
                    ExpirationDate = r.ExpirationDate,
                    CurrentQuantity = r.CurrentQuantity,
                    DaysRemaining =
                        EF.Functions.DateDiffDay(today, r.ExpirationDate!.Value).ToString()
                })
                .ToListAsync();
        }


        public async Task<List<ReagentLowStockDTO>> GetLowStockReagentsAsync(int? categoryId, decimal percent, bool expired)
        {
            var query = _dbSet
            .Where(r => r.IsActive && r.CurrentQuantity < r.MinQuantity)
            .AsQueryable();

            // Фильтр по категории
            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            // Исключить просроченные
            if (expired)
                query = query.Where(r =>
                    r.ExpirationDate == null ||
                    r.ExpirationDate >= DateTime.Today);


            query = query.Where(r =>
                (r.CurrentQuantity * 100m / r.MinQuantity) < percent);

            return await query
                .OrderBy(r => r.CurrentQuantity / r.MinQuantity)
                .Select(r => new ReagentLowStockDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CurrentQuantity = r.CurrentQuantity,
                    MinQuantity = r.MinQuantity,
                    Unit = r.Unit,
                    CriticalPercent = (r.CurrentQuantity / r.MinQuantity) * 100
                })
                .ToListAsync();
        }


        public async Task<List<ReagentPredictionDTO>> GetConsumptionHistoryAsync(int daysLookback = 90)
        {
            var startDate = DateTime.UtcNow.AddDays(-daysLookback);

            return await _dbSet
                .Select(r => new ReagentPredictionDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CurrentQuantity = r.CurrentQuantity,
                    MinQuantity = r.MinQuantity,
                    Unit = r.Unit,
                    ExpirationDate = r.ExpirationDate,
                    DaysToExpiry = r.ExpirationDate.HasValue
                        ? EF.Functions.DateDiffDay(DateTime.UtcNow, r.ExpirationDate.Value) : 999,
                    AvgDailyConsumption = r.Operations
                        .Where(o => o.OperationDate >= startDate && o.Quantity < 0) //OperationTypeId == 2
                        .Select(o => (decimal?)Math.Abs(o.Quantity))
                        .Average() ?? 0
                }).ToListAsync();
        }


        public async Task<int> GetActiveCountAsync()
            => await _dbSet.CountAsync(r => r.IsActive);

        public async Task<double> GetLowStockPercentageAsync()
        {
            var total = await _dbSet.CountAsync(r => r.IsActive);
            if (total == 0) return 0;
            var low = await _dbSet.CountAsync(r => r.IsActive && r.CurrentQuantity < r.MinQuantity);
            return Math.Round((double)low / total * 100, 1);
        }


        public async Task<double> GetExpiredPercentageAsync()
        {
            var totalWithDate = await _dbSet.CountAsync(r => r.IsActive && r.ExpirationDate != null);
            if (totalWithDate == 0) return 0;
            var expired = await _dbSet.CountAsync(r => r.IsActive && r.ExpirationDate < DateTime.UtcNow);
            return Math.Round((double)expired / totalWithDate * 100, 1);
        }


        public async Task<double> GetExpiringSoonPercentageAsync()
        {
            var now = DateTime.UtcNow;
            var in30Days = now.AddDays(30);

            // Считаем только те, у которых в принципе указан срок годности
            var totalWithDate = await _dbSet
                .CountAsync(r => r.IsActive && r.ExpirationDate != null);

            if (totalWithDate == 0) return 0;

            var expiringSoon = await _dbSet
                .CountAsync(r => r.IsActive &&
                                 r.ExpirationDate >= now &&
                                 r.ExpirationDate <= in30Days);

            return Math.Round((double)expiringSoon / totalWithDate * 100, 1);
        }


        // Сомнительные //////////////////////////////////////////////////
        public async Task<double> GetIlliquidPercentageAsync(int days = 90)
        {
            var deadline = DateTime.UtcNow.AddDays(-days);
            var totalActive = await _dbSet.CountAsync(r => r.IsActive);
            if (totalActive == 0) return 0;

            // Находим ID реагентов, по которым были движения
            var activeIds = await _context.ReagentOperations
                .Where(o => o.OperationDate >= deadline)
                .Select(o => o.ReagentId)
                .Distinct()
                .ToListAsync();

            var illiquidCount = await _dbSet
                .CountAsync(r => r.IsActive && !activeIds.Contains(r.Id));

            return Math.Round((double)illiquidCount / totalActive * 100, 1);
        }


        public async Task<double> GetDsiDaysAsync(int days = 90)
        {
            var startDate = DateTime.UtcNow.AddDays(-days);

            var consumption = await _context.ReagentOperations
                .Where(o => o.OperationDate >= startDate && o.Quantity < 0) // Сомнительно
                .SumAsync(o => Math.Abs(o.Quantity));

            if (consumption == 0) return 0;

            var currentStock = await _dbSet.Where(r => r.IsActive).SumAsync(r => r.CurrentQuantity);

            // Формула: (Средний остаток / Расход) * Период
            // Здесь используем текущий остаток как упрощенный средний
            return Math.Round((double)(currentStock / consumption) * days, 0);
        }


        public async Task<List<ReagentExpirationDTO>> GetUpcomingExpirationsAsync(int count = 5)
        {
            var now = DateTime.UtcNow.Date;

            var reagents = await _dbSet
                .Where(r => r.IsActive && r.ExpirationDate >= now)
                .OrderBy(r => r.ExpirationDate)
                .Take(count)
                .ToListAsync();

            return reagents.Select(r => new ReagentExpirationDTO
            {
                Id = r.Id,
                Name = r.Name,
                ExpirationDate = r.ExpirationDate,
                DaysRemaining = FormatExpirationLabel(r.ExpirationDate!.Value )
            }).ToList();
        }

    }
}
