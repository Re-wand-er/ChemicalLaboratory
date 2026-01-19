using Microsoft.AspNetCore.Mvc;
using ChemicalLaboratory.WebApi.Contracts;
using System.Diagnostics.Contracts;

namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly ApiClient _apiClient;
        private readonly ILogger<OperationTypeController> _logger;
        public OperationTypeController(ApiClient apiClient, ILogger<OperationTypeController> logger) 
        {
            _apiClient = apiClient;
            _logger = logger;
        }
    }
}
