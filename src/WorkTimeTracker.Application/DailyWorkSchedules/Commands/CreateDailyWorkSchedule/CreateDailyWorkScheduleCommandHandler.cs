using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByEmployeeIdMonthDailyWorkSchedules;
using WorkTimeTracker.Domain.Interfaces.Repositories;
using WorkTimeTracker.Domain.Interfaces.Services;
using WorkTimeTracker.Domain.Utils;
using WorkTimeTracker.Domain.Services;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.CreateDailyWorkSchedule
{
    internal class CreateDailyWorkScheduleCommandHandler : IRequestHandler<CreateDailyWorkScheduleCommand>
    {
        private readonly IDailyWorkScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IHolidaysProvider _holidaysProvider;

        public CreateDailyWorkScheduleCommandHandler
            (IDailyWorkScheduleRepository repository, IMapper mapper, IMediator mediator, IHolidaysProvider holidaysProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _holidaysProvider = holidaysProvider;
        }

        public async Task Handle(CreateDailyWorkScheduleCommand request, CancellationToken cancellationToken)
        {
            var holidays = await _holidaysProvider.GetHolidaysAsync(request.Date.Year);
            var holidaysDates = holidays.Select(h => h.Date).ToList();

            var norm = WorkTimeCalculator.CalculateWorkingTimeDimension(
                request.Date.StartOfMonth(),
                request.Date.EndOfMonth(),
                holidaysDates).hours;

            var dailyWorkShedule = await _mediator
                .Send(new GetMonthDailyWorkSchedulesByEmployeeIdQuery(request.EmployeeId, request.Date.Year, request.Date.Month), cancellationToken);

            decimal plannedWorkHours = dailyWorkShedule.Sum(d => d.WorkHours.Minutes) / 60;

            //if (norm < plannedWorkHours + (request.WorkHours.Minutes / 60))...

        }



    }
}
