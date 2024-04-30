using MediatR;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.CreateDailyWorkSchedule
{
    public class CreateDailyWorkScheduleCommand : IRequest
    {
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TypeOfDay TypeOfDay { get; set; }
        public DateTime PlannedWorkStart { get; set; }
        public DateTime PlannedWorkEnd { get; set; }
        public EmployeeDetailsDto? Employee { get; set; }
    }
}
