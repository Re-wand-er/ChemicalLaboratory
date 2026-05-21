using ChemicalLaboratory.Application.UseCases.DTOs.Filters;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.DTOs;
using Mapster;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class ReagentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly GeneratePDFService _generatePdfService;
        private readonly QrDecoderService _qrDecoderService;
        private readonly ILogger<ReagentService> _logger;

        public ReagentService
        (
            IUnitOfWork unitOfWork, 
            ICurrentUserService currentUserService, 
            GeneratePDFService generatePdfService, 
            QrDecoderService qrDecoderService,
            ILogger<ReagentService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _generatePdfService = generatePdfService;
            _qrDecoderService = qrDecoderService;
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

                await _unitOfWork.Reagents.SoftDeleteAsync(ids);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task RestoreAsync(IEnumerable<int> ids)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

            foreach (var id in ids)
            {
                var history = ReagentOperation.CreateForUpdate(userId, id, "Восстановление данных");
                await _unitOfWork.ReagentOperations.AddAsync(history);
            }

            await _unitOfWork.Reagents.RestoreAsync(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ReagentDTO> UpdateAsync(ReagentUpdateDTO dto)
        {
            var userId = _currentUserService.GetRequiredUserId();

            _logger.LogInformation($"Updating reagent with id: {dto.Id}");

            var existingReagent = await _unitOfWork.Reagents.GetByIdAsync(dto.Id);
            if (existingReagent == null) 
                throw new KeyNotFoundException("Reagent not found");

            dto.Adapt(existingReagent);

            var historyEntry = ReagentOperation.Create(
                userId, 
                OperationTypeEnum.Update, 
                existingReagent, 
                "Редактирование параметров реактива"
            );

            await _unitOfWork.ReagentOperations.AddAsync(historyEntry);
            await _unitOfWork.SaveAsync();

            if(existingReagent.CurrentQuantity < existingReagent.MinQuantity){
                var quantityNotification = Notification.Create(
                    userId,
                    existingReagent,
                    "Low_Quantity", // нужно менять
                    $"Низкий остаток реактива {existingReagent.Name}",
                    $"Низкое количество реактива {existingReagent.Name}."+ 
                    $"Необходимо пополнить запас до {existingReagent.ExpirationDate?.ToString("dd.MM.yyyy") ?? DateTime.MinValue.ToString()}"
                );

                await _unitOfWork.Notifications.AddAsync(quantityNotification);
                await _unitOfWork.SaveAsync();
            }

            // CategoryName почему-то пустой
            return existingReagent.Adapt<ReagentDTO>(); 
        }

        public async Task<List<ReagentDTO>> UpdateBatchAsync(
            List<ReagentUpdateDTO> dtos)
        {
            var userId = _currentUserService.GetRequiredUserId();
            var updatedReagents = new List<ReagentDTO>();
            var lowStockReagets = new List<ReagentDTO>();

            foreach (var dto in dtos)
            {
                var reagent = await _unitOfWork.Reagents.GetByIdAsync(dto.Id);

                if (reagent == null)
                    throw new KeyNotFoundException(
                        $"Reagent with id {dto.Id} not found");

                dto.Adapt(reagent);

                var historyEntry = ReagentOperation.Create(
                    userId,
                    OperationTypeEnum.Update,
                    reagent,
                    "Массовое редактирование реактивов"
                );

                await _unitOfWork.ReagentOperations.AddAsync(historyEntry);

                if(reagent.CurrentQuantity < reagent.MinQuantity)
                    lowStockReagets.Add(reagent.Adapt<ReagentDTO>());

                updatedReagents.Add(reagent.Adapt<ReagentDTO>());
            }

            await _unitOfWork.SaveAsync();

            string pdfRelativePath = await _generatePdfService.GenerateLowStockInvoicePdfAsync(lowStockReagets);

            if (!string.IsNullOrEmpty(pdfRelativePath))
            {
                var batchNotification = Notification.Create(
                    userId,
                    null,
                    "Low_Quantity", 
                    "Обнаружен дефицит реагентов",
                    "Сформирована накладная на закупку.",
                    pdfRelativePath 
                );

                await _unitOfWork.Notifications.AddAsync(batchNotification);
                await _unitOfWork.SaveAsync();
            }

            return updatedReagents;
        }


        public async Task<string> CreateOrderInvoiceFromInputsAsync(List<ReagentOrderInputDTO> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
                return string.Empty;

            var reagentDTOs = new List<ReagentDTO>();

            foreach (var item in orderItems)
            {
                var reagent = await GetByIdAsync(item.Id);
                if (reagent != null)
                {
                    var baseDto = reagent.Adapt<ReagentDTO>();

                    var modifiedDto = baseDto with 
                    { 
                        MinQuantity = item.Quantity, 
                        CurrentQuantity = 0 
                    };

                    reagentDTOs.Add(modifiedDto);
                }
            }

            return await _generatePdfService.GenerateLowStockInvoicePdfAsync(reagentDTOs);
        }
  


        public async Task<List<ReagentDTO>> ProcessIncomeFromQrImagesAsync(List<Stream> imageStreams)
        {
            var userId = _currentUserService.GetRequiredUserId();

            var recognizedItems = await _qrDecoderService.DecodeReagentQrCodesAsync(imageStreams);

            if (!recognizedItems.Any())
                throw new InvalidOperationException("На загруженных фотографиях не обнаружено читаемых QR-кодов.");

            var aggregatedItems = recognizedItems
                .GroupBy(x => x.Id)
                .Select(g => new QrReagentData(g.Key, g.Sum(x => x.Quantity)))
                .ToList();

            var updateDtos = new List<ReagentUpdateDTO>();

            // 2. Для каждого распознанного QR запрашиваем текущие данные из БД и готовим UpdateDTO
            foreach (var item in aggregatedItems)
            {
                var reagent = await _unitOfWork.Reagents.GetByIdAsync(item.Id);
                if (reagent == null)
                    throw new KeyNotFoundException($"Реактив с ID {item.Id} из QR-кода не найден в системе.");

                // Рассчитываем новое количество (Приход: Текущее + Из QR)
                decimal newQuantity = reagent.CurrentQuantity + item.Quantity;

                // Создаем DTO для обновления, сохраняя старые параметры, но меняя количество
                var dto = new ReagentUpdateDTO(
                    Id: reagent.Id,
                    Name: reagent.Name,
                    ChemicalFormula: reagent.ChemicalFormula,
                    Unit: reagent.Unit,
                    CurrentQuantity: newQuantity, // Наше новое количество
                    MinQuantity: reagent.MinQuantity,
                    ExpirationDate: reagent.ExpirationDate,
                    StorageLocation: reagent.StorageLocation,
                    CategoryId: reagent.CategoryId,
                    IsActive: reagent.IsActive
                );

                updateDtos.Add(dto);
            }

            // 3. Вызываем твой пакетный метод UpdateBatchAsync!
            // Он сам запишет историю как "Массовое редактирование реактивов", 
            // проверит дефицит и сгенерирует накладную, если это необходимо.
            return await UpdateBatchAsync(updateDtos);
        }



        public async Task<ReagentStockReportDTO> GetStockReportAsync()
        {
            _logger.LogInformation("Запрос распределения запасов по категориям");
            return await _unitOfWork.Reagents.GetStockDistributionReportAsync();
        }

        public async Task<List<ReagentExpirationDTO>> GetExpiringReagentsReportAsync(ExpirationFilterDTO filter)
            => await _unitOfWork.Reagents.GetExpiringReagentsAsync(filter.Status, filter.CategoryId, filter.DaysAhead, filter.OnlyWithStock);

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


        public async Task<List<ReagentReportDTO>> GetReagentReportAsync(int? categoryId)
            => await _unitOfWork.Reagents.GetReagentReportAsync(categoryId);






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


        public async Task<List<TopUsedReagentDTO>> GetTopUsedReportAsync(TopUsedFilterDTO filter)
            => await _unitOfWork.ReagentOperations.GetTopUsedReagentsAsync(
                filter.DateFrom,
                filter.DateTo,
                filter.Top,
                filter.CategoryId,
                filter.MinUsage);


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

        public async Task<List<IncomingReportDTO>> GetReagentOperationsReportAsync(IncomingReportFilterDTO filter, OperationTypeEnum operationType)
            => await _unitOfWork.ReagentOperations
                .GetReagentOperationsReportAsync(filter.DateFrom, filter.DateTo, filter.CategoryId, filter.ReagentId, filter.MinQuantity, operationType);

    }
}
