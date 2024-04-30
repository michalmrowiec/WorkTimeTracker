using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule;
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
                dtos.Add(
                    await _mediator.Send(new GetMonthlySummaryForEmployeeQuery(workSchedule.Key.Id, request.Year, request.Month), cancellationToken),
                    _mapper.Map<List<DailyWorkScheduleDto>>(workSchedule.Value));
            }

            foreach (var workSchedule in dtos.Values)
            {
                foreach (var item in workSchedule)
                {
                    item.RealWorkTime = TimeSpan.FromMinutes(
                        item.ActionTimes?.Where(x => x.IsWork && x.TimeOfAction.HasValue).Sum(x => x.TimeOfAction!.Value.TotalMinutes) ?? 0);

                    item.RealBreakTime = TimeSpan.FromMinutes(
                        item.ActionTimes?.Where(x => !x.IsWork && x.TimeOfAction.HasValue).Sum(x => x.TimeOfAction!.Value.TotalMinutes) ?? 0);

                    if(item.ActionTimes?.Where(x => x.IsWork)?.Any() ?? false)
                    {
                        item.RealWorkStart = item.ActionTimes?.Where(x => x.IsWork).Min(x => x.Start);
                    }

                    if (item.ActionTimes?.Where(x => x.IsWork)?.Any(x => x.End.HasValue) ?? false)
                    {
                        item.RealWorkEnd = item.ActionTimes?.Where(x => x.IsWork).Min(x => x.End);
                    }

                    //item.Overtime = item.WorkTimeNorm - item.RealWorkTime; // to change

                    await _mediator.Send(_mapper.Map<UpdateDailyWorkScheduleCommand>(item));
                }
            }

            return dtos.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkScheduleDto>);
        }
    }
}
