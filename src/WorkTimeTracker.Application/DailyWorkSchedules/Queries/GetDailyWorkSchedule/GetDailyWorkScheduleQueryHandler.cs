using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetDailyWorkSchedule
{
    internal class GetDailyWorkScheduleQueryHandler :
        IRequestHandler<GetDailyWorkScheduleQuery, IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        public GetDailyWorkScheduleQueryHandler(IDailyWorkScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>> Handle(
            GetDailyWorkScheduleQuery request, CancellationToken cancellationToken)
        {
            var workSchedules = await _repository.Get(request.EmployeeId, request.Year, request.Month);
            var dtos = new Dictionary<EmployeeDto, List<DailyWorkScheduleDto>>();

            foreach (var workSchedule in workSchedules)
            {
                dtos.Add(_mapper.Map<EmployeeDto>(workSchedule.Key),
                    _mapper.Map<List<DailyWorkScheduleDto>>(workSchedule.Value));
            }
            //var dtos = _mapper.Map<IDictionary<EmployeeDto, IEnumerable<DailyWorkScheduleDto>>>(workSchedules);
            //var dtos = workSchedules
            //    .ToDictionary(k => _mapper.Map<EmployeeDto>(k.Key), v => _mapper.Map<DailyWorkScheduleDto>(v.Value));

            //return dtos.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkScheduleDto>);

            return dtos.ToDictionary(k => k.Key, v => v.Value as IEnumerable<DailyWorkScheduleDto>);
        }
    }
}
