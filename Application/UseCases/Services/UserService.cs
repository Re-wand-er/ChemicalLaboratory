using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Infrastructure.Email;
using ChemicalLaboratory.WebApi.Models;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IDistributedCache _cache; // кэш для хранения кода
        private readonly IJwtService _jwtService;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IDistributedCache cache, IJwtService jwtService, IEmailSender emailSender, IPasswordHasher passwordHasher, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _cache = cache;
            _jwtService = jwtService;
            _emailSender = emailSender;
            _passwordHasher = passwordHasher;
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

        public async Task<AuthResponse?> LoginAsync(string login, string password) 
        {
            _logger.LogInformation($"Get user with login: {login}");

            var user = await _unitOfWork.Users.GetByLoginAsync(login);
            if (user == null || !user.IsActive || !_passwordHasher.VerifyHash(password, user.PasswordHash))
                return null;

            var userDto = user.Adapt<UserReadDTO>();
            var token = _jwtService.GenerateToken(userDto);

            return new AuthResponse(userDto, token);
        }

        public async Task AddAsync(UserCreateDTO dto)
        {
            _logger.LogInformation($"Creating user with FirstName={dto.FirstName} with MiddleName={dto.MiddleName}");
            
            var user = dto.Adapt<User>();
            user.PasswordHash = _passwordHasher.HashPassword(dto.PasswordHash);

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

        public async Task<bool> ResetPasswordAsync(string email, string code, string password)
        {
            var savedCode = await _cache.GetStringAsync(email);
            if (savedCode == null || savedCode != code) return false;

            string passwordHash = _passwordHasher.HashPassword(password);
            await _unitOfWork.Users.UpdatePasswordAsync(email, passwordHash);
            await _unitOfWork.SaveAsync();

            await _cache.RemoveAsync(email);
            return true;
        }

        public async Task SendResetCodeAsync(string email)
        {
            if (!await _unitOfWork.Users.IfExistByEmailAsync(email)) return;

            string code = new Random().Next(100000, 999999).ToString();
            await _cache.SetStringAsync(email, code, 
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });

            string subject = "Код идентификации:";
            string body = "<h2>Пожалуйста введите ваш код в окне приложения.</h2>" +
                          "<h3>Ваш идентификационный номер: </h3>" + code + "" +
                          "<h3>Будьте осторожны код дейcтвует 10 минут с момента получения письма. </h3>";
            await _emailSender.SendMailToEmail(email, subject, body);
        }
    }
}
