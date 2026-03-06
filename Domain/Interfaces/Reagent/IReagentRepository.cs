using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentRepository : IBaseRepository<Reagent>
    {
        Task AddRangeAsync(IEnumerable<Reagent> reagentDTOs);
    }
}
