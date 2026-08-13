using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }

        public Task AddRangeAsync(IEnumerable<Supplier> equipmentDTOs)
        {
            throw new NotImplementedException();
        }
    }
}
