using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByEmployeeIdMonthDailyWorkSchedules
{
    internal class GetMonthDailyWorkSchedulesByEmployeeIdQueryHandler
        : IRequestHandler<GetMonthDailyWorkSchedulesByEmployeeIdQuery, IEnumerable<DailyWorkScheduleDto>>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMonthDailyWorkSchedulesByEmployeeIdQueryHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<DailyWorkScheduleDto>> Handle(GetMonthDailyWorkSchedulesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _repository.GetByEmployeeId(request.EmployeeId, request.Year, request.Month);

            var dtos = _mapper.Map<List<DailyWorkScheduleDto>>(schedules);

            return dtos;
        }
    }
}
