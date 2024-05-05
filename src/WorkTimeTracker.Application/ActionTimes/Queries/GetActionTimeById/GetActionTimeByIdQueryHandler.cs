using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.ActionTimes.Queries.GetActionTimeById
{
    internal class GetActionTimeByIdQueryHandler : IRequestHandler<GetActionTimeByIdQuery, ActionTimeDto>
    {
        private readonly IActionTimeRepository _repository;
        private readonly IMapper _mapper;

        public GetActionTimeByIdQueryHandler(IActionTimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ActionTimeDto> Handle(GetActionTimeByIdQuery request, CancellationToken cancellationToken)
        {
            var actionTime = await _repository.GetActionTimeById(request.Id);
            var actionTimeDto = _mapper.Map<ActionTimeDto>(actionTime);

            return actionTimeDto;
        }
    }
}
