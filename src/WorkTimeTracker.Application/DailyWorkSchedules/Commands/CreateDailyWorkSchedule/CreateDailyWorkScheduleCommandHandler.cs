using AutoMapper;
using MediatR;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByEmployeeIdMonthDailyWorkSchedules;
using WorkTimeTracker.Domain.Interfaces.Repositories;
using WorkTimeTracker.Domain.Interfaces.Services;
using WorkTimeTracker.Domain.Utils;

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

            var norm = CalculateWorkingTimeDimension(
                request.Date.StartOfMonth(),
                request.Date.EndOfMonth(),
                holidaysDates);

            var dailyWorkShedule = await _mediator
                .Send(new GetMonthDailyWorkSchedulesByEmployeeIdQuery(request.EmployeeId, request.Date.Year, request.Date.Month), cancellationToken);

            decimal plannedWorkHours = dailyWorkShedule.Sum(d => d.WorkHours.Minutes) / 60;

            //if (norm < plannedWorkHours + (request.WorkHours.Minutes / 60))...

        }

        public int CalculateWorkingTimeDimension(DateTime periodStart, DateTime periodEnd, List<DateTime> holidays)
        {
            int numberOfWeeks = (periodEnd - periodStart).Days / 7;
            int daysToEndOfPeriod = (periodEnd - periodStart).Days % 7;
            int daysFromMondayToFriday = daysToEndOfPeriod > 5 ? 5 : daysToEndOfPeriod;
            int numberOfHolidays = holidays.Count(d => d.DayOfWeek != DayOfWeek.Sunday && d >= periodStart && d <= periodEnd);

            int workingTimeDimension = 40 * numberOfWeeks + 8 * daysFromMondayToFriday - 8 * numberOfHolidays;

            return workingTimeDimension;
        }

    }
}
