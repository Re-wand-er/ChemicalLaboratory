namespace ChemicalLaboratory.Domain.DTOs.ReagentsDTO
{
    public record ReagentUsageTrendDTO
    {
        // Список имен ТОП-5 реагентов (для генерации <Line /> на фронте)
        public List<string> TopReagentNames { get; set; } = new();
        // Данные для графика: [{ "name": "2024-01", "Кислота": 10, "Щелочь": 5 }, ...]
        public List<Dictionary<string, object>> ChartData { get; set; } = new();
    }

}
