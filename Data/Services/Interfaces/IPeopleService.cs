using EFCore.DTOs;

namespace EFCore.Services
{
    public interface IPeopleService
    {
        Task AddAsync(PeopleDTO entity);
        Task AddRangeAsync(List<PeopleDTO>? peopleDTOs);
        Task<bool> UpdateAsync(PeopleDTO entity);
        //Task DeleteAsync(PeopleDTO entity);
        Task DeleteAsync(int id);
        Task<List<PeopleDTO>> GetAllAsync();
        //Task<PeopleDTO> GetAllByIdAsync(int id);
        //Task<WorkSchedule> GetAllWorkScheduledAsync();

    }
}
