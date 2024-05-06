using MediatR;
using WorkTimeTracker.Application.ActionTimes.Commands.CreateActionTime;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.CalcTimesForDailyWorkSchedule;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands.CreateEndOfActionTime
{
    internal class CreateEndOfActionTimeCommandHandler : IRequestHandler<CreateEndOfActionTimeCommand>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IDailyWorkScheduleRepository _repositoryWs;
        private readonly IMediator _mediator;

        public CreateEndOfActionTimeCommandHandler(IActionTimeRepository repository, IDailyWorkScheduleRepository repositoryWs, IMediator mediator)
        {
            _repository = repository;
            _repositoryWs = repositoryWs;
            _mediator = mediator;
        }
        public async Task Handle(CreateEndOfActionTimeCommand request, CancellationToken cancellationToken)
        {
            request.End = request.End.AddSeconds(-request.End.Second);

            var incompleteActionTimes = await _repository.GetAllIncompleteActionTimesForEmployee(request.EmployeeId);

            if (!incompleteActionTimes.Where(x => x.IsWork == request.IsWork).Any())
            {
                await _mediator.Send(new CreateActionTimeCommand()
                {
                    EmployeeId = request.EmployeeId,
                    IsWork = request.IsWork,
                    Start = request.End,
                    End = request.End
                });

                return;
            }

            var actionTimeToUpdate = incompleteActionTimes.Last();
            actionTimeToUpdate.End = request.End;
            actionTimeToUpdate.TimeOfAction = request.End - actionTimeToUpdate.Start;

            var ws = await _repositoryWs.GetByEmployeeId(request.EmployeeId, actionTimeToUpdate.Start.Year, actionTimeToUpdate.Start.Month);
            DailyWorkSchedule? wds = ws.FirstOrDefault(x => x.Date.Date == actionTimeToUpdate.Start.Date);
            if (wds != null)
            {
                actionTimeToUpdate.DailyWorkScheduleId = wds.Id;
            }

            await _repository.UpdateActionTimeAsync(actionTimeToUpdate);

            if (actionTimeToUpdate.DailyWorkScheduleId != null)
            {
                await _mediator.Send(new CalcTimesForDailyWorkScheduleCommand(actionTimeToUpdate.DailyWorkScheduleId));
            }

            return;
        }
    }
}
