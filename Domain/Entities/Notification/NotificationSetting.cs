namespace ChemicalLaboratory.Domain.Entities
{
    public class NotificationSetting
    {
        public int Id { get; set; }
        public decimal LowQuantityThreshold { get; set; }
        public int ExpirationDaysThreshold { get; set; }
        public int AnalyticsUpdateInterval { get; set; }
        public string? EmailTemplate { get; set; }
    }
}
