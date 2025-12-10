namespace ChemicalLaboratory.WebUI.Models.Peoples
{
    public class WorkScheduleViewModel
    {
        public int IdWorkSchedule { get; set; }
        public string WorkShift { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}