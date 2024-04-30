using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Commands.UpdateActionTime
{
    internal class UpdateActionTimeCommandHandler : IRequestHandler<UpdateActionTimeCommand>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IDailyWorkScheduleRepository _repositoryWs;
        private readonly IMapper _mapper;

        public UpdateActionTimeCommandHandler(IActionTimeRepository repository, IDailyWorkScheduleRepository repositoryWs, IMapper mapper)
        {
            _repository = repository;
            _repositoryWs = repositoryWs;
            _mapper = mapper;
        }

        public async Task Handle(UpdateActionTimeCommand request, CancellationToken cancellationToken)
        {
            var ws = await _repositoryWs.GetByEmployeeId(request.EmployeeId, request.Start.Year, request.Start.Month);
            DailyWorkSchedule? wds = ws.FirstOrDefault(x => x.Date.Date == request.Start.Date);

            var actionTimeMapped = _mapper.Map<ActionTime>(request); 

            if (wds != null)
            {
                actionTimeMapped.DailyWorkScheduleId = wds.Id;
            }

            actionTimeMapped.TimeOfAction = actionTimeMapped.End - actionTimeMapped.Start;
            
                await _repository.UpdateActionTimeAsync(actionTimeMapped);
            return;
        }
    }
}
