using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IDailyWorkScheduleRepository
    {
        /// <summary>
        /// GetByDepartment includes child departments.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetByDepartment(string departmentId, int year, int month);

        Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetAll(int year, int month);

        Task<IEnumerable<DailyWorkSchedule>> GetByEmployeeId(string employeeId, int year, int month);
        Task<DailyWorkSchedule?> GetById(string id);

        Task CreateDailyWorkSchedule(DailyWorkSchedule dailyWorkSchedule);
        Task UpdateDailyWorkSchedule(DailyWorkSchedule dailyWorkSchedule);

    }
}
