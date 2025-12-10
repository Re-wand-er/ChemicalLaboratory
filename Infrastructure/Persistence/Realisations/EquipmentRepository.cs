using Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class EquipmentRepository: IEquipmentRepository
    {
        private readonly DataBaseContext _context;
        public EquipmentRepository(DataBaseContext context) => _context = context;

        public async Task<List<EquipmentDTO>> GetAllEquipmentAsync() 
        {
            return await _context.Equipments
                .AsNoTracking()
                .Select(e => new EquipmentDTO 
                {
                    Id = e.idEquipment,
                    Model = e.Model,
                    Name = e.Name,
                    Description = e.Description,
                    Kind = e.Kind,
                    Status = e.Status,
                })
                .ToListAsync();
        }

        public Task AddRangeAsync(IEnumerable<EquipmentDTO> equipmentDTOs) { throw new NotImplementedException(); }
        public Task<bool> UpdateAsync() { throw new NotImplementedException(); }
        public Task DeleteAsync(int id) { throw new NotImplementedException(); }
        public Task DeleteAsync(EquipmentDTO equipment) { throw new NotImplementedException(); }

    }
}
