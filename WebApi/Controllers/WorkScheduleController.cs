using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/work-schedule")]
    public class WorkScheduleController : ControllerBase
    {
        private readonly IWorkScheduleRepository _workSchedule;
        private readonly ILogger<WorkScheduleController> _logger;
        public WorkScheduleController(IWorkScheduleRepository workSchedule, ILogger<WorkScheduleController> logger)
        {
            _workSchedule = workSchedule;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories() => Ok(await _workSchedule.GetAllAsync());

    }
}
