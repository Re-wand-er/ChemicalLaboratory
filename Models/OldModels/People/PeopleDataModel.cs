using ChemicalLaboratory.Models.Experiment;
using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.People
{
    public class PeopleDataModel
    {
        public int IdPeople { get; set; } = 0;
        public int idWorkShedule { get; set; } = 0;
        public int idExperiment { get; set; } = 0;
        public ExperimentDataModel IdExperiment { get; set; } = new ExperimentDataModel();
        public WorkScheduleDataModel IdWorkShedule { get; set; } = new WorkScheduleDataModel();
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";
        public string Sex { get; set; } = "";
        public string SystemRole { get; set; } = "";
        public string JobPosition { get; set; } = "";

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; } = "";

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$")]
        public string? Password { get; set; }

        public PeopleDataModel() { }

    }
}
