using Microsoft.AspNetCore.Mvc;
using ChemicalLaboratory.WebApi.Contracts;
using System.Diagnostics.Contracts;

namespace ChemicalLaboratory.WebApi.Controllers
{
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
