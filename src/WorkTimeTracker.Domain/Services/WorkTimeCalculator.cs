using WorkTimeTracker.Domain.Utils;

namespace WorkTimeTracker.Domain.Services
{
    public static class WorkTimeCalculator
    {
        public static (int hours, int freeDays) CalculateWorkingTimeDimension(DateTime periodStart, DateTime periodEnd, List<DateTime> holidays)
        {
            int tot = 0;
            int freeDays = 0;

            for (DateTime dt = periodStart; dt <= periodEnd; dt = dt.AddDays(1))
            {
                Console.WriteLine($"{dt.ToShortDateString()} | {dt.DayOfWeek}");
                if (dt.DayOfWeek == DayOfWeek.Saturday && holidays.Contains(dt))
                {
                    tot -= 8;
                    freeDays += 2;

                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(dt))
                {

                    freeDays++;
                    continue;
                }
                else
                {
                    tot += 8;
                }
            }

            return new(tot, freeDays);
        }

        public static (int hours, int freeDays) CalculateWorkingTimeDimension(int year, int month, List<DateTime> holidays)
        {
            var date = new DateTime(year, month, 1);

            return CalculateWorkingTimeDimension(date.StartOfMonth(), date.EndOfMonth(), holidays);
        }

    }
}
