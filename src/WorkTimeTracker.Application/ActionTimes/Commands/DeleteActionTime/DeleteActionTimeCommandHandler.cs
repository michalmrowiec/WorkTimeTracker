using MediatR;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.CalcTimesForDailyWorkSchedule;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands.DeleteActionTime
{
    internal class DeleteActionTimeCommandHandler : IRequestHandler<DeleteActionTimeCommand>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IMediator _mediator;


        public DeleteActionTimeCommandHandler(IActionTimeRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(DeleteActionTimeCommand request, CancellationToken cancellationToken)
        {
            var actionTime = await _repository.GetActionTimeById(request.Id);

            if (actionTime == null)
            {
                return;
            }

            await _repository.DeleteActionTime(actionTime);

            if (actionTime.DailyWorkScheduleId != null)
            {
                await _mediator.Send(new CalcTimesForDailyWorkScheduleCommand(actionTime.DailyWorkScheduleId));
            }
        }
    }
}
