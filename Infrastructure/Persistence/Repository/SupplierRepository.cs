using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repository
{
    public class SupplierRepository: ISupplierRepository
    {
        private readonly DataBaseContext _context;
        public SupplierRepository(DataBaseContext context) => _context = context;

        public Task<List<SuppplierDTO>> GetAllEquipmentAsync() 
        {
            //return await _context.Equipments
            //    .AsNoTracking()
            //    .Select(e => new EquipmentDTO 
            //    {
            //        Id = e.idEquipment,
            //        Model = e.Model,
            //        Name = e.Name,
            //        Description = e.Description,
            //        Kind = e.Kind,
            //        Status = e.Status,
            //    })
            //    .ToListAsync();
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<SuppplierDTO> equipmentDTOs) { throw new NotImplementedException(); }
        public Task<bool> UpdateAsync() { throw new NotImplementedException(); }
        public Task DeleteAsync(int id) { throw new NotImplementedException(); }
        public Task DeleteAsync(SuppplierDTO equipment) { throw new NotImplementedException(); }

    }
}
