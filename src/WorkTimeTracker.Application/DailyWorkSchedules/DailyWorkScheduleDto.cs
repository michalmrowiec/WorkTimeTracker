using System.ComponentModel.DataAnnotations;
using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Application.DailyWorkSchedules
{
    public class DailyWorkScheduleDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime PlannedWorkStart { get; set; }
        [Required]
        public DateTime PlannedWorkEnd { get; set; }
        public TimeSpan WorkTimeNorm { get; set; }
        public TimeSpan BreakTimeNorm { get; set; }
        public DateTime RealWorkStart { get; set; }
        public DateTime RealWorkEnd { get; set; }
        public TimeSpan WorkHours { get; set; }
        public TimeSpan NightWorkHours { get; set; }
        public TimeSpan OverTime { get; set; }
        public TimeSpan NightOvertime { get; set; }
        public TimeSpan OvertimeCollected { get; set; }
        public EmployeeDto? Employee { get; set; }
    }
}
