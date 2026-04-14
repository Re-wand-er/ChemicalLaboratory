using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface IReagentCategoryRepository :IBaseRepository<ReagentCategory>
    {
        Task<IEnumerable<ListItemDTO>> GetAllIdNameAsync(); 
    }
}
