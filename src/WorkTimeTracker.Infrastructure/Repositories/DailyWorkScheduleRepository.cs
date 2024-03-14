using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.ApplicationUser;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class DailyWorkScheduleRepository : IDailyWorkScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DailyWorkScheduleRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> Get(string reportsToEmployeeId, int year, int month)
        {
            var employees = await _context.Employees
                .Where(e => e.ReportsToId!.Equals(reportsToEmployeeId) || e.Id.Equals(reportsToEmployeeId))
                .ToListAsync();

            var schedules = new Dictionary<Employee, List<DailyWorkSchedule>>();

            foreach (var employee in employees)
            {
                schedules[employee] = await _context.DailyWorkSchedules
                    .Where(schedule => schedule.EmployeeId == employee.Id && schedule.Date.Month == month && schedule.Date.Year == year)
                    .OrderBy(schedule => schedule.Date)
                    .ToListAsync();
            }

            return schedules.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkSchedule>);
        }

        //public async Task<IEnumerable<DailyWorkSchedule>> GetDailyWorkSchedulesForReportsTo(
        //    string reportsToEmployeeId, int year, int month)
        //{
        //    return await _context.DailyWorkSchedules
        //        .Include(d => d.Employee)
        //        .Where(d => d.Employee!.ReportsToId!.Equals(reportsToEmployeeId) && d.Date.Month == month && d.Date.Year == year)
        //        .OrderBy(schedule => schedule.Date)
        //        .ToListAsync();
        //}

        //// TODO
        //public async Task<IEnumerable<DailyWorkSchedule>> GetViaRole(string employeeId)
        //{
        //    Employee employee = (Employee)await _userManager.FindByIdAsync(employeeId);
        //    var roles = await _userManager.GetRolesAsync(employee!);

        //    Dictionary<Roles, Func<string, Task<IEnumerable<DailyWorkSchedule>>>> strategy = new()
        //    {
        //        {
        //            Roles.Admin,
        //            async (employeeId) =>
        //            {
        //                return await _context.DailyWorkSchedules
        //                .ToListAsync();
        //            }
        //        },
        //        {
        //            Roles.Director,
        //            async (employeeId) =>
        //            {
        //                return await _context.DailyWorkSchedules
        //                .ToListAsync();
        //            }
        //        },
        //        {
        //            Roles.HR,
        //            async (employeeId) =>
        //            {
        //                return await _context.DailyWorkSchedules
        //                .ToListAsync();
        //            }
        //        },
        //        {
        //            Roles.Manager,
        //            async (employeeId) =>
        //            {
        //                return await _context.DailyWorkSchedules
        //                .Include(d => d.Employee)
        //                .Where(d => d.Employee!.ReportsToId!.Equals(employee!.ReportsToId))
        //                .ToListAsync();
        //            }
        //        }
        //    };

        //    return await strategy[roles.GetHighestRole()].Invoke(employeeId);

        //}
    }
}
