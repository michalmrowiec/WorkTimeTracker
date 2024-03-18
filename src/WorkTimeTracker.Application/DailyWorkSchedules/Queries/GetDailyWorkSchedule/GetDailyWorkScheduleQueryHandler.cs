using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetDailyWorkSchedule
{
    internal class GetDailyWorkScheduleQueryHandler :
        IRequestHandler<GetDailyWorkScheduleQuery, IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetDailyWorkScheduleQueryHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>> Handle(
            GetDailyWorkScheduleQuery request, CancellationToken cancellationToken)
        {
            var employee = await _mediator.Send(new GetEmployeeDetailsQuery(request.EmployeeId));

            var workSchedules = await _repository.Get(employee.Department?.Id ?? string.Empty, request.Year, request.Month);
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
