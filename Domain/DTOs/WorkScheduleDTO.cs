namespace Domain.DTOs
{
    public record WorkScheduleDTO
    {
        public int idWorkSchedule { get; set; }
        public string WorkShift { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
