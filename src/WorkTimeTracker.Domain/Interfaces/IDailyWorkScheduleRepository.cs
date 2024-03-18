using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces
{
    public interface IDailyWorkScheduleRepository
    {
        //Task<IEnumerable<DailyWorkSchedule>> GetViaRole(string employeeId);
        //Task<IEnumerable<DailyWorkSchedule>> GetDailyWorkSchedulesForReportsTo(string departmentId, int year, int month);
        Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> Get(string departmentId, int year, int month);
    }
}
