using MediatR;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule
{
    internal class UpdateDailyWorkScheduleCommandHandler : IRequestHandler<UpdateDailyWorkScheduleCommand>
    {
        private readonly IDailyWorkScheduleRepository _repository;

        public UpdateDailyWorkScheduleCommandHandler(IDailyWorkScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDailyWorkScheduleCommand request, CancellationToken cancellationToken)
        {
            request.PlannedWorkStart = request.PlannedWorkStart.AddSeconds(-request.PlannedWorkStart.Second);
            request.PlannedWorkEnd = request.PlannedWorkEnd.AddSeconds(-request.PlannedWorkEnd.Second);

            if (request.PlannedWorkStart > request.PlannedWorkEnd)
            {
                return;
            }

            var dws = await _repository.GetById(request.Id);
            if (dws == null)
            {
                return;
            }

            dws.TypeOfDay = request.TypeOfDay;
            dws.PlannedWorkStart = request.PlannedWorkStart;
            dws.PlannedWorkEnd = request.PlannedWorkEnd;
            dws.PlannedWorkTime = request.PlannedWorkEnd - request.PlannedWorkStart;
            dws.RealWorkTime = request.RealWorkTime;
            dws.RealBreakTime = request.RealBreakTime;
            dws.RealOvertimeMinutes = request.RealOvertimeMinutes;

            if (dws.PlannedWorkTime > TimeSpan.FromHours(16))
            {
                dws.PlannedBreakTime = TimeSpan.FromMinutes(45);
            }
            else if (dws.PlannedWorkTime > TimeSpan.FromHours(9))
            {
                dws.PlannedBreakTime = TimeSpan.FromMinutes(30);
            }
            else if (dws.PlannedWorkTime > TimeSpan.FromHours(6))
            {
                dws.PlannedBreakTime = TimeSpan.FromMinutes(15);
            }

            await _repository.UpdateDailyWorkSchedule(dws);
        }
    }
}
