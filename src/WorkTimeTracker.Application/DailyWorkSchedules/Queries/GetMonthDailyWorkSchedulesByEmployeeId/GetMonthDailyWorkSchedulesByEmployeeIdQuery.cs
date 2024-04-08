using MediatR;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByEmployeeIdMonthDailyWorkSchedules
{
    public record GetMonthDailyWorkSchedulesByEmployeeIdQuery(string EmployeeId, int Year, int Month) : IRequest<IEnumerable<DailyWorkScheduleDto>>;
}
