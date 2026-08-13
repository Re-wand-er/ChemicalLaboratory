using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/system-role")]
    public class SystemRoleController : ControllerBase
    {
        private readonly ISystemRoleRepository _systemRole;
        private readonly ILogger<SystemRoleController> _logger;
        public SystemRoleController(ISystemRoleRepository systemRole, ILogger<SystemRoleController> logger)
        {
            _systemRole = systemRole;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemRoles() => Ok(await _systemRole.GetAllAsync());

    }
}
