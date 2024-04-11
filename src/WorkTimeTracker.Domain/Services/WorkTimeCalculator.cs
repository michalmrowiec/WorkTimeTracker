using WorkTimeTracker.Domain.Utils;

namespace WorkTimeTracker.Domain.Services
{
    public static class WorkTimeCalculator
    {
        public static int CalculateWorkingTimeDimension(DateTime periodStart, DateTime periodEnd, List<DateTime> holidays)
        {
            int numberOfWeeks = (periodEnd - periodStart).Days / 7;
            int daysToEndOfPeriod = (periodEnd - periodStart).Days % 7;
            int daysFromMondayToFriday = daysToEndOfPeriod > 5 ? 5 : daysToEndOfPeriod;
            int numberOfHolidays = holidays.Count(d => d.DayOfWeek != DayOfWeek.Sunday && d >= periodStart && d <= periodEnd);

            int workingTimeDimension = 40 * numberOfWeeks + 8 * daysFromMondayToFriday - 8 * numberOfHolidays;

            return workingTimeDimension;
        }

        public static int CalculateWorkingTimeDimension(int year, int month, List<DateTime> holidays)
        {
            var date = new DateTime(year, month, 1);

            return CalculateWorkingTimeDimension(date.StartOfMonth(), date.EndOfMonth(), holidays);
        }

    }
}
