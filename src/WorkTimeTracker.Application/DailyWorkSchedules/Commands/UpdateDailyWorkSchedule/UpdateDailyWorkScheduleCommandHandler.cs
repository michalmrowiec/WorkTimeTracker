using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule
{
    internal class UpdateDailyWorkScheduleCommandHandler : IRequestHandler<UpdateDailyWorkScheduleCommand>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateDailyWorkScheduleCommandHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task Handle(UpdateDailyWorkScheduleCommand request, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<DailyWorkSchedule>(request);
            await _repository.UpdateDailyWorkSchedule(mapped);
        }
    }
}
