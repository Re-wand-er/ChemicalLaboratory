using ChemicalLaboratory.Domain.Enums;

namespace ChemicalLaboratory.Infrastructure.Persistence
{
    public static class VisualTimeFormater
    {
        public static string FormatRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return "только что";

            if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes switch
                {
                    1 => "минуту назад",
                    2 or 3 or 4 => $"{timeSpan.Minutes} минуты назад",
                    _ => $"{timeSpan.Minutes} минут назад"
                };

            if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours switch
                {
                    1 => "час назад",
                    2 or 3 or 4 => $"{timeSpan.Hours} часа назад",
                    _ => $"{timeSpan.Hours} часов назад"
                };

            if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days switch
                {
                    1 => "вчера",
                    _ => $"{timeSpan.Days} дней назад"
                };

            return dateTime.ToString("dd.MM.yyyy");
        }

        public static string FormatDateByPeriod(DateTime date, ReportPeriod step) => step switch
        {
            ReportPeriod.Day => date.ToString("dd.MM"),
            ReportPeriod.Week => $"Нед {System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}",
            ReportPeriod.Month => date.ToString("MMM yyyy"),
            _ => date.ToString("dd.MM")
        };

        public static string GetRelativeTime(DateTime dateTime)
        {
            var span = DateTime.UtcNow - dateTime;
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} мин. назад";
            if (span.TotalHours < 24) return $"{(int)span.TotalHours} час. назад";
            return dateTime.ToString("dd.MM HH:mm");
        }

        public static string FormatExpirationLabel(DateTime date)
        {
            var days = (date.Date - DateTime.UtcNow.Date).Days;
            return days switch
            {
                0 => "Сегодня",
                1 => "Завтра",
                _ when days < 7 => $"{date:dd MMM} ({days} дн.)",
                _ => date.ToString("dd.MM.yyyy")
            };
        }
    }
}
