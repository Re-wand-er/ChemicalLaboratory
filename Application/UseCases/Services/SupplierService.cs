using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using Mapster;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class SupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(IUnitOfWork unitOfWork, ILogger<SupplierService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllAsync(bool includeInactive = false)
        {
            _logger.LogInformation("Get all Suppliers");
            var Suppliers = await _unitOfWork.Suppliers.GetAllAsync(includeInactive);
            return Suppliers.Adapt<IEnumerable<SupplierDTO>>();
        }

        public async Task<SupplierDTO?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get supplier with id: {id}");
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
            if (supplier == null)
            {
                _logger.LogWarning($"Supplier with id = {id} not found");
                return null;
            }

            return supplier.Adapt<SupplierDTO>();
        }

        public async Task AddAsync(SupplierDTO dto)
        {
            _logger.LogInformation($"Creating supplier with Name={dto.Name}");
            var supplier = dto.Adapt<Supplier>();

            await _unitOfWork.Suppliers.AddAsync(supplier);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            _logger.LogInformation($"Deleted supplier with ids SupplierService");

            await _unitOfWork.Suppliers.SoftDeleteAsync(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task RestoreAsync(IEnumerable<int> ids)
        {
            _logger.LogInformation($"Deleted supplier with ids SupplierService");

            await _unitOfWork.Suppliers.RestoreAsync(ids);
            await _unitOfWork.SaveAsync();
        }

        public async Task<SupplierDTO> UpdateAsync(SupplierDTO dto)
        {
            _logger.LogInformation($"Updated supplier with id: {dto.Id}");

            var existingSupply = await _unitOfWork.Suppliers.GetByIdAsync(dto.Id);
            if (existingSupply == null) throw new Exception("Supplier not found");

            dto.Adapt(existingSupply);

            await _unitOfWork.SaveAsync();

            return existingSupply.Adapt<SupplierDTO>();
        }
    }
}
