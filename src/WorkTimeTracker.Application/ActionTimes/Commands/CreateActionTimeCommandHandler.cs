using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands
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
            var wds = ws.FirstOrDefault(x => x.Date.Date == request.Start.Date);

            if (request.IsWork)
            {
                WorkActionTime workActionTime = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = request.EmployeeId,
                    Start = request.Start,
                    End = request.End,
                    TimeOfAction = request.End - request.Start,
                    DailyWorkScheduleId = wds?.Id
                };

                await _repository.CreateActionTimeAsync(workActionTime);
                return;
            }
            else
            {
                BreakActionTime breakActionTime = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = request.EmployeeId,
                    Start = request.Start,
                    End = request.End,
                    TimeOfAction = request.End - request.Start,
                    DailyWorkScheduleId = wds?.Id
                };

                await _repository.CreateActionTimeAsync(breakActionTime);
                return;
            }
        }
    }
}
