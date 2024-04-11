using MediatR;
using WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules
{
    public record GetMonthDailyWorkSchedulesByDepartmentQuery(string DepartmentId, int Year, int Month)
        : IRequest<IDictionary<MonthlyScheduleEmployeeDto, IEnumerable<DailyWorkScheduleDto>>>;
}
