using MediatR;
using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules
{
    public record GetByDepartmentDailyWorkSchedulesQuery(string DepartmentId, int Year, int Month)
        : IRequest<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>;
}
