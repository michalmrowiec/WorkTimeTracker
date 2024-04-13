using System.Globalization;

namespace WorkTimeTracker.MVC
{
    public interface IDisplayService
    {
        /// <summary>
        /// Returns the name of the month corresponding to the given integer.
        /// </summary>
        /// <param name="month">An integer representing the month (1-12).</param>
        /// <returns>The name of the month in the current culture's language.</returns>
        /// <remarks>For example, for the "en-US" culture, DisplayNameOfMonth(1) will return "January".</remarks>
        string DisplayNameOfMonth(int month);

        /// <summary>
        /// Returns the name of the month and the year of the given DateTime object.
        /// </summary>
        /// <param name="dateTime">A DateTime object.</param>
        /// <returns>The name of the month and the year in the current culture's language.</returns>
        /// <remarks>For example, for the "en-US" culture, DisplayNameOfMonthAndYear(new DateTime(2024, 1, 1)) will return "January 2024".</remarks>
        string DisplayNameOfMonthAndYear(DateTime dateTime);
    }

    public class DisplayService : IDisplayService
    {
        private readonly CultureInfo _culture;

        public DisplayService(CultureInfo cultureInfo)
        {
            _culture = cultureInfo;
        }

        public string DisplayNameOfMonth(int month)
        {
            if (month < 1 || month > 12)
                return "";

            return _culture.DateTimeFormat.GetMonthName(month);
        }
        public string DisplayNameOfMonthAndYear(DateTime dateTime) => dateTime.ToString("MMMM yyyy", _culture);

    }
}
