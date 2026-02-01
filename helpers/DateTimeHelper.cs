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
    }
}
