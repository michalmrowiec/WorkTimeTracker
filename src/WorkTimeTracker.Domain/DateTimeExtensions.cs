namespace WorkTimeTracker.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfMonth(this DateTime dateTime)
            => new(dateTime.Date.Year, dateTime.Date.Month, 1);

        public static DateTime EndOfMonth(this DateTime dateTime)
            => new(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
    }
}
