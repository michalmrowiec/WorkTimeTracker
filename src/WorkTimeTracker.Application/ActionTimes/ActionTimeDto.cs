using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.ActionTimes
{
    public class ActionTimeDto
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? TimeOfAction { get; set; }
        public string? DailyWorkScheduleId { get; set; }
        public DailyWorkSchedule? DailyWorkSchedule { get; set; }
        public bool IsWork {  get; set; }
    }
}
