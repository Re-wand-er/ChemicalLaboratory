using ChemicalLaboratory.Domain.DTOs;
using ChemicalLaboratory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context) => _context = context;

        public Task AddAsync(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }

        // нужен маппинг dto с entity
        public Task AddRangeAsync(List<PeopleDTO>? peopleDTOs)
        {
            //if (peopleDTOs is null) throw new ArgumentNullException();

            //try
            //{
            //    var peopleEntities = peopleDTOs.Select(dto => new User
            //    {
            //        idPeople       = dto.IdPeople,
            //        FirstName      = dto.FirstName ?? string.Empty,
            //        MiddleName     = dto.MiddleName ?? string.Empty,
            //        LastName       = dto.LastName ?? string.Empty,
            //        Sex            = dto.Sex ?? throw new InvalidDataException(), 
            //        Email          = dto.Email ?? throw new InvalidOperationException("Email is required"),
            //        JobPosition    = dto.JobPosition,
            //        SystemRole     = dto.SystemRole,
            //        Login          = dto.Login ?? throw new InvalidOperationException("Login is required"),
            //        PasswordHash   = null, 
            //        idExperiment   = null,
            //        idWorkSchedule = dto.WorkSchedule?.idWorkSchedule ?? 0 
            //    }).ToList();

            //    await _context.Peoples.AddRangeAsync(peopleEntities);
            //    await _context.SaveChangesAsync(); 
            //}
            //catch
            //{
            //    throw new OperationCanceledException();
            //}
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PeopleDTO updatedPeople)
        {
            //var existing = await _context.Peoples.FindAsync(updatedPeople.IdPeople);

            //if (existing == null)
            //    return false;
            //try
            //{
            //    // Переделать
            //    existing.FirstName = updatedPeople.FirstName ?? "FirstName";
            //    existing.MiddleName = updatedPeople.MiddleName!;
            //    existing.LastName = updatedPeople.LastName!;
            //    existing.Sex = updatedPeople.Sex!;
            //    existing.Email = updatedPeople.Email!;
            //    existing.JobPosition = updatedPeople.JobPosition;
            //    existing.SystemRole = updatedPeople.SystemRole;
            //    existing.Login = updatedPeople.Login!;

            //    await _context.SaveChangesAsync();
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}

            throw new NotImplementedException();
        }

        public Task DeleteAsync(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            //User? people = await _context.Peoples.FirstOrDefaultAsync(p => p.idPeople == id);
            //if (people != null)
            //{
            //    _context.Peoples.Remove(people);
            //    // в идеале обработчик ошибок
            //    await _context.SaveChangesAsync();
            //}

            throw new NotImplementedException();
        }

        // маппинг вручную
        public Task<List<PeopleDTO>> GetAllAsync()
        {
            //var peopleList = await _context.Peoples
            //    .AsNoTracking()
            //    .Include(p => p.WorkSchedule)
            //    .Select(p => new PeopleDTO
            //    {
            //        IdPeople = p.idPeople,
            //        FirstName = p.FirstName,
            //        MiddleName = p.MiddleName,
            //        LastName = p.LastName,
            //        Sex = p.Sex,
            //        Email = p.Email,
            //        JobPosition = p.JobPosition,
            //        SystemRole = p.SystemRole,
            //        Login = p.Login,

            //        WorkSchedule = p.WorkSchedule != null ? new WorkScheduleDTO
            //        {
            //            idWorkSchedule = p.WorkSchedule.idWorkSchedule,
            //            WorkShift = p.WorkSchedule.WorkShift,
            //            StartTime = p.WorkSchedule.StartTime,
            //            EndTime = p.WorkSchedule.EndTime
            //        } : null
            //    })
            //    .ToListAsync();

            //return peopleList;
            throw new NotImplementedException();
        }

        public Task<PeopleDTO> GetAllByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public Task<WorkSchedule> GetAllWorkScheduledAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
