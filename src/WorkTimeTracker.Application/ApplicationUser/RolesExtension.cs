namespace WorkTimeTracker.Application.ApplicationUser
{
    public static class RolesExtension
    {
        public static Roles GetHighestRole(this IList<string> roles)
        {
            if (!roles.Any())
            {
                return Roles.Employee;
            }

            var roleEnums = roles.Select(r => Enum.Parse<Roles>(r));
            var highestRole = roleEnums.Min();

            return highestRole;
        }
    }

    public enum Roles
    {
        Admin = 0,
        Director = 1,
        HR = 3,
        Manager = 4,
        Employee = 5
    }
}
