using MediatR;
using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetAllDailyWorkSchedules
{
    public record GetAllDailyWorkSchedulesQuery(int Year, int Month)
        : IRequest<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>;
}
