using MediatR;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Domain.Interfaces.Repositories;
using WorkTimeTracker.Domain.Interfaces.Services;
using WorkTimeTracker.Domain.Services;

namespace WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee
{
    internal class GetMonthlySummaryForEmployeeQueryHandler : IRequestHandler<GetMonthlySummaryForEmployeeQuery, MonthlyScheduleEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDailyWorkScheduleRepository _dailyWorkScheduleRepository;
        private readonly IMediator _mediator;
        private readonly IHolidaysProvider _holidaysProvider;

        public GetMonthlySummaryForEmployeeQueryHandler(
            IEmployeeRepository employeeRepository,
            IDailyWorkScheduleRepository dailyWorkScheduleRepository,
            IMediator mediator,
            IHolidaysProvider holidaysProvider)
        {
            _employeeRepository = employeeRepository;
            _dailyWorkScheduleRepository = dailyWorkScheduleRepository;
            _mediator = mediator;
            _holidaysProvider = holidaysProvider;
        }

        public async Task<MonthlyScheduleEmployeeDto> Handle(GetMonthlySummaryForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employeeDetails = await _mediator.Send(new GetEmployeeDetailsQuery(request.EmployeeId));


            var holidays = await _holidaysProvider.GetHolidaysAsync(request.Year);
            var holidaysDates = holidays.Select(h => h.Date).ToList();

            var monthlyHourNorm = WorkTimeCalculator.CalculateWorkingTimeDimension(request.Year, request.Month, holidaysDates);
            var sumOfPlannedWorkHours = await _dailyWorkScheduleRepository.GetByEmployeeId(request.EmployeeId, request.Year, request.Month);

            var monthlyScheduleEmployeeDto = new MonthlyScheduleEmployeeDto()
            {
                Email = employeeDetails.Email,
                Department = employeeDetails.Department.Name,
                Roles = employeeDetails.Roles,
                FirstName = employeeDetails.FirstName,
                LastName = employeeDetails.LastName,
                Id = employeeDetails.Id,
                Workload = employeeDetails.Workload,
                Year = request.Year,
                Month = request.Month,
                MonthlyHourNorm = new TimeSpan(monthlyHourNorm, 0, 0),
                SumOfPlannedWorkHours = new TimeSpan(0, sumOfPlannedWorkHours.Sum(d => d.WorkTimeNorm.Minutes), 0),
                SumOfNightWorkHours = new TimeSpan(0, sumOfPlannedWorkHours.Sum(d => d.NightWorkHours.Minutes), 0),
                SumOfOvertime = new TimeSpan(),
                SumOfNightOvertime = new TimeSpan(),
                SumOfOvertimeCollected = new TimeSpan(),
                SumOfRealWorkHours = new TimeSpan()
            };

            return monthlyScheduleEmployeeDto;
        }
    }
}
