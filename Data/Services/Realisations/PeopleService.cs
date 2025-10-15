using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.Services;
using EFCore.Entities;
using EFCore.DTOs;
using ChemicalLaboratory.Models.NewModels;

namespace EFCore.Services
{
    internal class PeopleService : IPeopleService
    {
        private readonly DataBaseContext _context;

        public PeopleService(DataBaseContext context) => _context = context;

        public Task<PeopleDTO> AddAsync(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PeopleDTO updatedPeople)
        {
            var existing = await _context.Peoples.FindAsync(updatedPeople.IdPeople);

            if (existing == null)
                return false;
            try
            {
                // Переделать
                existing.FirstName   = updatedPeople.FirstName ?? "FirstName";
                existing.MiddleName  = updatedPeople.MiddleName!;
                existing.LastName    = updatedPeople.LastName!;
                existing.Sex         = updatedPeople.Sex!;
                existing.Email       = updatedPeople.Email!;
                existing.JobPosition = updatedPeople.JobPosition;
                existing.SystemRole  = updatedPeople.SystemRole;
                existing.Login       = updatedPeople.Login!;
               
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<PeopleDTO> DeleteAsync(PeopleDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<PeopleDTO> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PeopleDTO>> GetAllAsync()
        {
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
