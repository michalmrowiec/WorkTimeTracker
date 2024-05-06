using MediatR;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.CalcTimesForDailyWorkSchedule;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands.CreateActionTime
{
    internal class CreateActionTimeCommandHandler : IRequestHandler<CreateActionTimeCommand>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IDailyWorkScheduleRepository _repositoryWs;
        private readonly IMediator _mediator;

        public CreateActionTimeCommandHandler(IActionTimeRepository repository,
            IDailyWorkScheduleRepository repositoryWs,
            IMediator mediator)
        {
            _repository = repository;
            _repositoryWs = repositoryWs;
            _mediator = mediator;
        }

        public async Task Handle(CreateActionTimeCommand request, CancellationToken cancellationToken)
        {
            request.Start = request.Start.AddSeconds(-request.Start.Second);
            request.End = request.End?.AddSeconds(-request.End?.Second ?? 0);

            var ws = await _repositoryWs.GetByEmployeeId(request.EmployeeId, request.Start.Year, request.Start.Month);
            DailyWorkSchedule? wds = ws.FirstOrDefault(x => x.Date.Date == request.Start.Date);


            if (wds == null)
            {
                wds = new DailyWorkSchedule
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = request.EmployeeId,
                    Date = request.Start.Date,
                    PlannedWorkStart = request.Start,
                    PlannedWorkEnd = request.End ?? request.Start,
                    TypeOfDay = TypeOfDay.Unplanned
                };

                await _repositoryWs.CreateDailyWorkSchedule(wds);
            }

            ActionTime actionTime = new()
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = request.EmployeeId,
                Start = request.Start,
                End = request.End,
                TimeOfAction = request.End - request.Start,
                DailyWorkScheduleId = wds.Id,
                IsWork = request.IsWork
            };

            await _repository.CreateActionTimeAsync(actionTime);
            await _mediator.Send(new CalcTimesForDailyWorkScheduleCommand(wds.Id));
            return;
        }
    }
}
