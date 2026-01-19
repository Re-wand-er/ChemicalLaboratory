using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class ReagentService
    {
        private readonly IReagentRepository _reagentRepository;
        private readonly ILogger<ReagentService> _logger;

        public ReagentService(IReagentRepository reagentRepository, ILogger<ReagentService> logger)
        {
            _reagentRepository = reagentRepository;
            _logger = logger;
        }
    }
}
