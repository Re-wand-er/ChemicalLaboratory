namespace ChemicalLaboratory.Domain.DTOs
{
    public record NotificationSideBarDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ReagentName { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
