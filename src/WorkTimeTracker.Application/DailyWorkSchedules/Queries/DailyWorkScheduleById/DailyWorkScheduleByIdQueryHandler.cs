using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.DailyWorkScheduleById
{
    internal class DailyWorkScheduleByIdQueryHandler
        : IRequestHandler<DailyWorkScheduleByIdQuery, DailyWorkScheduleDto?>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;

        public DailyWorkScheduleByIdQueryHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DailyWorkScheduleDto?> Handle
            (DailyWorkScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var dws = await _repository.GetById(request.Id);

            if(dws == null)
            {
                return null;
            }

            var mapped = _mapper.Map<DailyWorkScheduleDto>(dws);

            return mapped;
        }
    }
}
