using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.NewModels
{
    public class WorkSchedule
    {
        [Key]
        public int IdWorkSchedule { get; set; }
        public string WorkShift { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<People> People { get; set; }
    }
}