using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WorkTimeTracker.Domain.Utils
{
    public static class EnumToListDisplay
    {
        public static List<(string value, string displayName)> GetEnumValuesAndDisplayNames<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }

            var valuesAndDisplayNames = new List<(string, string)>();

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var value = field.Name;
                var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
                var displayName = displayAttribute?.Name ?? value;

                valuesAndDisplayNames.Add(new (value, displayName));
            }

            return valuesAndDisplayNames;
        }

    }
}
