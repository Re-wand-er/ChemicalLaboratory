namespace ChemicalLaboratory.Domain.DTOs
{
    public record DashboardDTO
    {
        
        public int ActiveReagentsCount { get; set; }
        public double LowStockPercentage { get; set; }
        public double ExpiredPercentage { get; set; }
        public double ExpiringSoonPercentage { get; set; }
        public int OperationsTodayCount { get; set; }
        public double IlliquidPercentage { get; set; } 
        public double DsiDays { get; set; }           

    }
}
