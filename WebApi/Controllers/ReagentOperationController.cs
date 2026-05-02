using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.DTOs.Filters;
using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using ChemicalLaboratory.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/reagent-operation")]
    public class ReagentOperationController : ControllerBase
    {
        private readonly ReagentService _reagentService;
        private readonly ILogger<ReagentOperationController> _logger;

        public ReagentOperationController(ReagentService reagentService, ILogger<ReagentOperationController> logger)
        {
            _reagentService = reagentService;
            _logger = logger;
        }


        [HttpGet("top-usage")]
        public async Task<IActionResult> GetTopUsage(
            [FromQuery] ReportPeriod period = ReportPeriod.Month,
            [FromQuery] int count = 5,
            [FromQuery] bool asc = false)
        {
            _logger.LogInformation("API: запрос топ-{Count} реагентов за {Period}", count, period);

            var result = await _reagentService.GetTopUsageReportAsync(period, count, asc);

            return Ok(result);
        }

        [HttpGet("top-used-report")]
        public async Task<ActionResult<List<TopUsedReagentDTO>>> GetTopUsed([FromQuery] TopUsedFilterDTO filter)
            => Ok(await _reagentService.GetTopUsedReportAsync(filter));


        [HttpGet("usage-trend")]
        public async Task<IActionResult> GetUsageTrend(
            [FromQuery] ReportPeriod period = ReportPeriod.Month,
            [FromQuery] ReportPeriod step = ReportPeriod.Week)
            => Ok(await _reagentService.GetUsageTrendReportAsync(period, step));


        [HttpGet("operations-stats")]
        public async Task<ActionResult<List<ItemDTO>>> GetStats(
            [FromQuery] OperationsGroupBy groupBy,
            [FromQuery] ReportPeriod period)
            => Ok(await _reagentService.GetOperationsCountReportAsync(groupBy, period));


        [HttpGet("average-size")]
        public async Task<ActionResult<List<ItemDTO>>> GetAverageSize([FromQuery] ReportPeriod period)
            => Ok(await _reagentService.GetAverageOperationSizeAsync(period));


        [HttpGet("turnover")]
        public async Task<ActionResult<List<ReagentTurnoverDTO>>> GetTurnover([FromQuery] ReportPeriod period)
           => Ok(await _reagentService.GetTurnoverReportAsync(period));


        [HttpGet("incoming")]
        public async Task<ActionResult<List<IncomingReportDTO>>> GetIncoming([FromQuery] IncomingReportFilterDTO filter)
            => Ok(await _reagentService.GetReagentOperationsReportAsync(filter, OperationTypeEnum.Receipt));


        [HttpGet("consumption")]
        public async Task<ActionResult<List<IncomingReportDTO>>> GetConsumption([FromQuery] IncomingReportFilterDTO filter)
            => Ok(await _reagentService.GetReagentOperationsReportAsync(filter, OperationTypeEnum.Consumption));


        [HttpGet("write-off")]
        public async Task<ActionResult<List<IncomingReportDTO>>> GetWriteOff([FromQuery] IncomingReportFilterDTO filter)
            => Ok(await _reagentService.GetReagentOperationsReportAsync(filter, OperationTypeEnum.WriteOff));


        [HttpGet("adjustment")]
        public async Task<ActionResult<List<IncomingReportDTO>>> GetAdjustment([FromQuery] IncomingReportFilterDTO filter)
           => Ok(await _reagentService.GetReagentOperationsReportAsync(filter, OperationTypeEnum.Adjustment));


        [HttpGet("update")]
        public async Task<ActionResult<List<IncomingReportDTO>>> GetUpdate([FromQuery] IncomingReportFilterDTO filter)
           => Ok(await _reagentService.GetReagentOperationsReportAsync(filter, OperationTypeEnum.Update));

    }
}
