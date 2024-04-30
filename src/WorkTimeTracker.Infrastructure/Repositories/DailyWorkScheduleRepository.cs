using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class DailyWorkScheduleRepository : IDailyWorkScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;

        public DailyWorkScheduleRepository
            (ApplicationDbContext context, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        public async Task CreateDailyWorkSchedule(DailyWorkSchedule dailyWorkSchedule)
        {
            await _context.DailyWorkSchedules
                .AddAsync(dailyWorkSchedule);

            await _context.SaveChangesAsync();
        }

        public async Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetAll(int year, int month)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .ToListAsync();

            var schedules = await GetDailyWorkSchedule(year, month, employees);

            return schedules;
        }

        public async Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetByDepartment(string departmentId, int year, int month)
        {
            if (departmentId == null || departmentId == string.Empty)
            {
                return new Dictionary<Employee, List<DailyWorkSchedule>>()
                    .ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkSchedule>);
            }

            var entireDepartments = (await _departmentRepository.GetDepartmentWithChilds(departmentId))
                .ToList()
                .Select(d => d.Id);

            var employees = await _context.Employees
                .Where(e => entireDepartments.Contains(e.DepartmentId ?? string.Empty))
                .AsNoTracking()
                .ToListAsync();

            var schedules = await GetDailyWorkSchedule(year, month, employees);

            return schedules;
        }

        public async Task<IEnumerable<DailyWorkSchedule>> GetByEmployeeId(string employeeId, int year, int month)
        {
            var schedules = await _context.DailyWorkSchedules
                .Where(schedule => schedule.EmployeeId == employeeId
                       && schedule.Date.Year == year
                       && schedule.Date.Month == month)
                .AsNoTracking()
                .ToListAsync();

            return schedules;
        }

        private async Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetDailyWorkSchedule(
            int year, int month, IList<Employee> employees)
        {
            var schedules = new Dictionary<Employee, List<DailyWorkSchedule>>();

            foreach (var employee in employees)
            {
                schedules[employee] = await _context.DailyWorkSchedules
                    .Include(schedule => schedule.ActionTimes)
                    .Where(schedule => schedule.EmployeeId == employee.Id && schedule.Date.Month == month && schedule.Date.Year == year)
                    .OrderBy(schedule => schedule.Date)
                    .AsNoTracking()
                    .ToListAsync();
            }

            return schedules.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkSchedule>);
        }

    }
}
