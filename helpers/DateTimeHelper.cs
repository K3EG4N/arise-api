namespace arise_api.helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetDateTimeNow()
        {
            var utcNow = DateTime.UtcNow;
            var peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcNow, peruTimeZone);
        }

        public static string FormatDateToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static DateTime ParseStringToDate(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            return DateTime.MinValue;
        }
    }
}
