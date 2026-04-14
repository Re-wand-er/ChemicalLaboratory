namespace ChemicalLaboratory.Application.UseCases.DTOs
{
    /// <summary>
    /// Это объект, который контроллер отправляет на фронтенд. 
    /// Он содержит вычисленные значения, которые видит пользователь в таблице.
    /// </summary>
    public class ReagentReportDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CurrentQuantity { get; set; }
        public decimal MinQuantity { get; set; }

        public string Unit { get; set; } = null!;
        // Результат расчета среднего или ML-прогноза
        public decimal AvgConsumption { get; set; }

        // Прогнозные сроки
        public int DaysToZero { get; set; }
        public int DaysToExpiry { get; set; }

        // Результат логики заказа
        public decimal RecommendedOrder { get; set; }

        // Расчитанная дата: Today + DaysToZero - LeadTime
        public DateOnly? OrderDeadline { get; set; }
    }
}
