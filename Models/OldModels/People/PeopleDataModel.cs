using ChemicalLaboratory.Models.Experiment;

namespace ChemicalLaboratory.Models.People
{
    public class PeopleDataModel 
	{
        public int IdPeople                        { get; set; } = 0;
        public int idWorkShedule                   { get; set; } = 0;
        public int idExperiment                    { get; set; } = 0;
        public ExperimentDataModel IdExperiment    { get; set; } = new ExperimentDataModel();
        public WorkScheduleDataModel IdWorkShedule { get; set; } = new WorkScheduleDataModel();
        public string FirstName                    { get; set; } = "";
        public string MiddleName                   { get; set; } = "";
        public string LastName                     { get; set; } = "";
        public string email                        { get; set; } = "";
        public string Sex                          { get; set; } = ""; 
        public string SystemRole                   { get; set; } = "";
        public string JobPosition                  { get; set; } = "";
        public string Login                        { get; set; } = "";
        public string PasswordHash                 { get; set; } = "";

        public PeopleDataModel() { }

	}
}
