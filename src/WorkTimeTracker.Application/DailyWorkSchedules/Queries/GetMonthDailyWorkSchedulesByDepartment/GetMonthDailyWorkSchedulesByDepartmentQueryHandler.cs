using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules
{
    internal class GetMonthDailyWorkSchedulesByDepartmentQueryHandler :
        IRequestHandler<GetMonthDailyWorkSchedulesByDepartmentQuery, IDictionary<MonthlyScheduleEmployeeDto, IEnumerable<DailyWorkScheduleDto>>>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMonthDailyWorkSchedulesByDepartmentQueryHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IDictionary<MonthlyScheduleEmployeeDto, IEnumerable<DailyWorkScheduleDto>>> Handle(
            GetMonthDailyWorkSchedulesByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var workSchedules = await _repository.GetByDepartment(request.DepartmentId, request.Year, request.Month);
            var dtos = new Dictionary<MonthlyScheduleEmployeeDto, List<DailyWorkScheduleDto>>();

            foreach (var workSchedule in workSchedules)
            {
                dtos.Add(/*(MonthlyScheduleEmployeeDto)_mapper.Map<EmployeeDto>(workSchedule.Key),*/
                    await _mediator.Send(new GetMonthlySummaryForEmployeeQuery(workSchedule.Key.Id, request.Year, request.Month), cancellationToken),
                    _mapper.Map<List<DailyWorkScheduleDto>>(workSchedule.Value));
            }

            return dtos.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkScheduleDto>);
        }
    }
}
