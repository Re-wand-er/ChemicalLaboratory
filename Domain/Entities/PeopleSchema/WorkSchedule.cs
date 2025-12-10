namespace Domain.Entities
{
    public class WorkSchedule
    {
        public int idWorkSchedule { get; set; }
        public string WorkShift { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public ICollection<People> Peoples { get; set; } = new List<People>();
    }
}
