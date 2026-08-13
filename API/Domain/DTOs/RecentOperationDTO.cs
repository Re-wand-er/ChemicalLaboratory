namespace ChemicalLaboratory.Domain.DTOs
{
    public class RecentOperationDTO
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string ActionDetails { get; set; } = string.Empty;
        public string RelativeTime { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; }
    }

}
