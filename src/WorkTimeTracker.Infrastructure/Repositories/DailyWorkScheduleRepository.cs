using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkTimeTracker.Application.ApplicationUser;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class DailyWorkScheduleRepository : IDailyWorkScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDepartmentRepository _departmentRepository;

        public DailyWorkScheduleRepository
            (ApplicationDbContext context, UserManager<IdentityUser> userManager, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _userManager = userManager;
            _departmentRepository = departmentRepository;
        }

        public async Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> Get(string departmentId, int year, int month)
        {
            if (departmentId == null || departmentId == string.Empty)
            {
                return new Dictionary<Employee, List<DailyWorkSchedule>>()
                    .ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkSchedule>);
            }

            //var childDepartments = await _departmentRepository.GetAllChildDepartments(departmentId);
            //var entireDepartments = childDepartments.Select(d => d.Id).Append(departmentId).ToList();
            var entireDepartments = (await _departmentRepository.GetDepartmentWithChilds(departmentId))
                .ToList()
                .Select(d => d.Id);

            var employees = await _context.Employees
                .Where(e => entireDepartments.Contains(e.DepartmentId ?? string.Empty))
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
    }
}
