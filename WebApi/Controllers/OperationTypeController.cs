using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly ILogger<OperationTypeController> _logger;
        public OperationTypeController(ILogger<OperationTypeController> logger) 
        {
            _logger = logger;
        }
    }
}
