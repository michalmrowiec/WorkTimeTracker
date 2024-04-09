using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules
{
    internal class GetByDepartmentDailyWorkSchedulesQueryHandler :
        IRequestHandler<GetMonthDailyWorkSchedulesByDepartmentQuery, IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetByDepartmentDailyWorkSchedulesQueryHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>> Handle(
            GetMonthDailyWorkSchedulesByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var workSchedules = await _repository.GetByDepartment(request.DepartmentId, request.Year, request.Month);
            var dtos = new Dictionary<EmployeeDto, List<DailyWorkScheduleDto>>();

            foreach (var workSchedule in workSchedules)
            {
                dtos.Add(_mapper.Map<EmployeeDto>(workSchedule.Key),
                    _mapper.Map<List<DailyWorkScheduleDto>>(workSchedule.Value));
            }

            return dtos.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkScheduleDto>);
        }
    }
}
