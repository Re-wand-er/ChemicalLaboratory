namespace ChemicalLaboratory.WebApi.Models
{
    public class LowStockFilterDTO
    {
        public int? CategoryId { get; set; } = null;
        public decimal CriticalPercent { get; set; } = 100;
        public bool ExcludeExpired { get; set; } = false;
    }
}
