using MediatR;
using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetDailyWorkSchedule
{
    public record GetDailyWorkScheduleQuery(string DepartmentId, int Year, int Month)
        : IRequest<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>;
}
