namespace ChemicalLaboratory.Domain.DTOs
{
    public class UserActivityDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int OperationsCount { get; set; }
    }

}
