using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.CreateDailyWorkSchedule
{
    internal class CreateDailyWorkScheduleCommandHandler : IRequestHandler<CreateDailyWorkScheduleCommand>
    {
        private readonly IDailyWorkScheduleRepository _repository;

        public CreateDailyWorkScheduleCommandHandler
            (IDailyWorkScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateDailyWorkScheduleCommand request, CancellationToken cancellationToken)
        {
            request.PlannedWorkStart = request.PlannedWorkStart.AddSeconds(-request.PlannedWorkStart.Second);
            request.PlannedWorkEnd = request.PlannedWorkEnd.AddSeconds(-request.PlannedWorkEnd.Second);

            if (request.PlannedWorkStart >= request.PlannedWorkEnd)
            {
                return;
            }

            var dailySchedule = new DailyWorkSchedule()
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = request.EmployeeId,
                Date = request.Date,
                TypeOfDay = request.TypeOfDay,
                PlannedWorkStart = request.PlannedWorkStart,
                PlannedWorkEnd = request.PlannedWorkEnd,
                PlannedWorkTime = request.PlannedWorkEnd - request.PlannedWorkStart
            };

            if (dailySchedule.PlannedWorkTime > TimeSpan.FromHours(16))
            {
                dailySchedule.PlannedBreakTime = TimeSpan.FromMinutes(45);
            }
            else if (dailySchedule.PlannedWorkTime > TimeSpan.FromHours(9))
            {
                dailySchedule.PlannedBreakTime = TimeSpan.FromMinutes(30);
            }
            else if (dailySchedule.PlannedWorkTime > TimeSpan.FromHours(6))
            {
                dailySchedule.PlannedBreakTime = TimeSpan.FromMinutes(15);
            }

            await _repository.CreateDailyWorkSchedule(dailySchedule);
        }

    }
}
