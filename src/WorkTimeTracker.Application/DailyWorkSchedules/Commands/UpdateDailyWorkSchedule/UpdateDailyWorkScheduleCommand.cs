using MediatR;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule
{
    public class UpdateDailyWorkScheduleCommand : DailyWorkScheduleDto, IRequest
    {
    }
}
