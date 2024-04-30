using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using WorkTimeTracker.Application.ActionTimes;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.DailyWorkSchedules
{
    public class DailyWorkScheduleDto
    {
        public string? Id { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TypeOfDay TypeOfDay { get; set; }

        [Required]
        public DateTime PlannedWorkStart { get; set; }
        [Required]
        public DateTime PlannedWorkEnd { get; set; }
        public TimeSpan WorkTimeNorm { get; set; }
        public TimeSpan BreakTimeNorm { get; set; }
        public DateTime? RealWorkStart { get; set; }
        public DateTime? RealWorkEnd { get; set; }
        public TimeSpan WorkHours { get; set; }
        public TimeSpan NightWorkHours { get; set; }
        public TimeSpan Overtime { get; set; }
        public TimeSpan NightOvertime { get; set; }
        public TimeSpan OvertimeCollected { get; set; }

        public TimeSpan RealWorkTime { get; set; }
        public TimeSpan RealBreakTime { get; set; }


        public EmployeeDto? Employee { get; set; }

        public List<ActionTimeDto>? ActionTimes { get; set; }
    }
}
