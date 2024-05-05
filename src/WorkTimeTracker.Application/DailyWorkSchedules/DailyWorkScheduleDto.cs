using System.ComponentModel.DataAnnotations;
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
        public TimeSpan PlannedWorkTime { get; set; }
        public TimeSpan PlannedBreakTime { get; set; }
        public DateTime? RealWorkStart { get; set; }
        public DateTime? RealWorkEnd { get; set; }
        public TimeSpan RealWorkTime { get; set; }
        public TimeSpan RealBreakTime { get; set; }
        public TimeSpan RealOvertime { get; set; }


        public EmployeeDto? Employee { get; set; }

        public List<ActionTimeDto>? ActionTimes { get; set; }
    }
}
