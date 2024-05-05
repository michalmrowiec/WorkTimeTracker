using MediatR;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.CalcTimesForDailyWorkSchedule
{
    internal class CalcTimesForDailyWorkScheduleCommandHandler : IRequestHandler<CalcTimesForDailyWorkScheduleCommand>
    {
        private readonly IDailyWorkScheduleRepository _repository;

        public CalcTimesForDailyWorkScheduleCommandHandler
            (IDailyWorkScheduleRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(CalcTimesForDailyWorkScheduleCommand request, CancellationToken cancellationToken)
        {

            var item = await _repository.GetById(request.Id);
            if (item == null)
            {
                return;
            }

            item.RealWorkTime = TimeSpan.FromMinutes(
                        item.ActionTimes?.Where(x => x.IsWork && x.TimeOfAction.HasValue).Sum(x => x.TimeOfAction!.Value.TotalMinutes) ?? 0);

            item.RealBreakTime = TimeSpan.FromMinutes(
                item.ActionTimes?.Where(x => !x.IsWork && x.TimeOfAction.HasValue).Sum(x => x.TimeOfAction!.Value.TotalMinutes) ?? 0);

            if (item.ActionTimes?.Where(x => x.IsWork)?.Any() ?? false)
            {
                item.RealWorkStart = item.ActionTimes?.Where(x => x.IsWork).Min(x => x.Start);
            }

            if (item.ActionTimes?.Where(x => x.IsWork)?.Any(x => x.End.HasValue) ?? false)
            {
                item.RealWorkEnd = item.ActionTimes?.Where(x => x.IsWork).Min(x => x.End);
            }

            double breakOvertime = 0;
            if(item.PlannedBreakTime.TotalMinutes < item.RealBreakTime.TotalMinutes)
            {
                breakOvertime = item.RealBreakTime.TotalMinutes - item.PlannedBreakTime.TotalMinutes;
            }
            var workOvertime = item.RealWorkTime - item.PlannedWorkTime;
            item.RealOvertimeMinutes = TimeSpan.FromMinutes(workOvertime.TotalMinutes - breakOvertime).TotalMinutes;

            await _repository.UpdateDailyWorkSchedule(item);
        }
    }
}
