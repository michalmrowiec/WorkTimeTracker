using MediatR;
using WorkTimeTracker.Application.ActionTimes;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule
{
    public class UpdateDailyWorkScheduleCommand : IRequest
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TypeOfDay TypeOfDay { get; set; }
        public DateTime PlannedWorkStart { get; set; }
        public DateTime PlannedWorkEnd { get; set; }
        public TimeSpan RealWorkTime { get; set; }
        public TimeSpan RealBreakTime { get; set; }
        public double RealOvertimeMinutes { get; set; }
        public EmployeeDetailsDto? Employee { get; set; }
        public List<ActionTimeDto>? ActionTimes { get; set; }
    }
}