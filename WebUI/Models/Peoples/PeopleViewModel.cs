using Domain.DTOs;

namespace ChemicalLaboratory.WebUI.Models.Peoples
{
    public class PeopleViewModel
    {
        public int IdPeople { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Sex { get; set; }
        public string? Email { get; set; }
        public string? SystemRole { get; set; }
        public string? JobPosition { get; set; }
        public WorkScheduleViewModel? WorkSchedule { get; set; }

        public PeopleViewModel(PeopleDTO peopleDTO)
        {
            IdPeople = peopleDTO.IdPeople;
            FirstName = peopleDTO.FirstName;
            MiddleName = peopleDTO.MiddleName;
            LastName = peopleDTO.LastName;
            Login = peopleDTO.Login;
            Sex = peopleDTO.Sex;
            Email = peopleDTO.Email;
            SystemRole = peopleDTO.SystemRole;
            JobPosition = peopleDTO.JobPosition;

            if (peopleDTO.WorkSchedule != null)
            {
                WorkSchedule = new WorkScheduleViewModel
                {
                    IdWorkSchedule = peopleDTO.WorkSchedule.idWorkSchedule,
                    WorkShift = peopleDTO.WorkSchedule.WorkShift,
                    StartTime = peopleDTO.WorkSchedule.StartTime,
                    EndTime = peopleDTO.WorkSchedule.EndTime
                };
            }
        }

        public PeopleViewModel() { }
    }
}