using MediatR;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.CalcTimesForDailyWorkSchedule
{
    public record CalcTimesForDailyWorkScheduleCommand(string Id) : IRequest;
}
