using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class ReagentRepository : BaseRepository<Reagent>, IReagentRepository
    {
        public ReagentRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public virtual Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync() =>
            await _dbSet
                .AsNoTracking()
                .Select(c => new ListItemDTO(c.Id, c.Name))
                .ToListAsync();

        public override async Task DeleteManyAsync(IEnumerable<int> ids)
        {
            var entities = await _context.Reagents
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

            _context.Reagents.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<ReagentStockReportDTO> GetStockDistributionReportAsync()
        {
            // 1. Получаем сгруппированные данные из БД (Категория + Реагент = Сумма)
            var rawData = await _context.Reagents
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

        public async Task<List<ReagentExpirationDTO>> GetExpiringReagentsAsync()
        {
            var today = DateTime.UtcNow.Date;
            var limitDate = today.AddDays(90);

            return await _context.Reagents
                .Where(r => r.IsActive && r.ExpirationDate != null && r.ExpirationDate <= limitDate)
                .OrderBy(r => r.ExpirationDate)
                .Select(r => new ReagentExpirationDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    ExpirationDate = r.ExpirationDate,
                    DaysRemaining = EF.Functions.DateDiffDay(today, r.ExpirationDate!.Value) // Срок годности может быть null
                })
                .ToListAsync();
        }

        public async Task<List<ReagentLowStockDTO>> GetLowStockReagentsAsync()
        {
            return await _context.Reagents
                .Where(r => r.IsActive && r.CurrentQuantity < r.MinQuantity)
                .OrderBy(r => r.CurrentQuantity / r.MinQuantity) // Сначала самые критичные (где % остатка меньше)
                .Select(r => new ReagentLowStockDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CurrentQuantity = r.CurrentQuantity,
                    MinQuantity = r.MinQuantity,
                    Unit = r.Unit
                })
                .ToListAsync();
        }


        public async Task<List<ReagentPredictionDTO>> GetConsumptionHistoryAsync(int daysLookback = 90)
        {
            var startDate = DateTime.UtcNow.AddDays(-daysLookback);

            return await _context.Reagents
                .Where(r => r.IsActive)
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

    }
}
