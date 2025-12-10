namespace Domain.DTOs
{
    public class ExperimentDTO
    {
        public int idExperiment { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }
    }
}
