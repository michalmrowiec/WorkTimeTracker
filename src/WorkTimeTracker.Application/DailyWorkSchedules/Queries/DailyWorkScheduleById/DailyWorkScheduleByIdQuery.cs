using MediatR;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.DailyWorkScheduleById
{
    public record DailyWorkScheduleByIdQuery(string Id) : IRequest<DailyWorkScheduleDto?>;
}
