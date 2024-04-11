using MediatR;

namespace WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee
{
    public record GetMonthlySummaryForEmployeeQuery(string EmployeeId, int Year, int Month) : IRequest<MonthlyScheduleEmployeeDto>;
}
