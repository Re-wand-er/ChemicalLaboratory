using EFCore.Entities;
using EFCore.DTOs;
using EFCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Services
{
    public interface IPeopleService : IBaseService<PeopleDTO>
    {
        Task<List<PeopleDTO>> GetAllAsync();
        Task<PeopleDTO> GetAllByIdAsync(int id);
        //Task<WorkSchedule> GetAllWorkScheduledAsync();
        
    }
}
