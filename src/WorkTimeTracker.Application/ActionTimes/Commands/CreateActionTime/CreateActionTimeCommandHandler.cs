using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands.CreateActionTime
{
    internal class CreateActionTimeCommandHandler : IRequestHandler<CreateActionTimeCommand>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IDailyWorkScheduleRepository _repositoryWs;

        public CreateActionTimeCommandHandler(IActionTimeRepository repository, IDailyWorkScheduleRepository repositoryWs)
        {
            _repository = repository;
            _repositoryWs = repositoryWs;
        }

        public async Task Handle(CreateActionTimeCommand request, CancellationToken cancellationToken)
        {
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
            return;
        }
    }
}
