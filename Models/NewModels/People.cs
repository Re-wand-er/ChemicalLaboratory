using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class People
    {
        [Key]
        public int IdPeople { get; set; }
        public int? IdExperiment { get; set; }
        public int IdWorkSchedule { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string SystemRole { get; set; }
        public string JobPosition { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual WorkSchedule WorkSchedule { get; set; }
    }
}