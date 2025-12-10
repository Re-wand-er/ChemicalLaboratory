namespace ChemicalLaboratory.Models.People
{
    public class WorkScheduleDataModel
    {
        public int idWorkSchedule { get; set; } = 0;
        public string? WorkShift { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
        public TimeSpan EndTime { get; set; } = TimeSpan.Zero;

        public WorkScheduleDataModel() { }
    }
}
