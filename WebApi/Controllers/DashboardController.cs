using ChemicalLaboratory.Application.UseCases.Services;
using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.DTOs.ReagentsDTO;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly ReagentService _reagentService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ReagentService reagentService, ILogger<DashboardController> logger)
        {
            _reagentService = reagentService;
            _logger = logger;
        }

        [HttpGet("dashboard-kpi")]
        public async Task<ActionResult<DashboardDTO>> GetDashboardKpi()
            => Ok(await _reagentService.GetMainDashboardKpiAsync());


        [HttpGet("recent-activity")]
        public async Task<ActionResult<List<RecentOperationDTO>>> GetRecentActivity()
            => Ok(await _reagentService.GetRecentActivityAsync());


        [HttpGet("expiration-calendar")]
        public async Task<ActionResult<List<ReagentExpirationDTO>>> GetCalendar()
            => Ok(await _reagentService.GetExpirationCalendarAsync());

        [HttpGet("user-activity")]
        public async Task<ActionResult<List<UserActivityDto>>> GetUserActivity([FromQuery] int days = 1)
            => Ok(await _reagentService.GetUserActivityTopAsync(days));
    }
}
