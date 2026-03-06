using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;
using Mapster;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsync()
        {
            _logger.LogInformation("Get all Users");
            var Users = await _unitOfWork.Users.GetAllAsync();
            return Users.Adapt<IEnumerable<UserReadDTO>>();
        }

        public async Task<UserReadDTO?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get user with id: {id}");
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with id = {id} not found");
                return null;
            }

            return user.Adapt<UserReadDTO>();
        }

        public async Task AddAsync(UserCreateDTO dto)
        {
            _logger.LogInformation($"Creating user with FirstName={dto.FirstName} with MiddleName={dto.MiddleName}");
            var user = dto.Adapt<User>();

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleted user with id: {id}");

            await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UserUpdateDTO dto)
        {
            _logger.LogInformation($"Updated user with id: {dto.Id}");
            var user = dto.Adapt<User>();

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
