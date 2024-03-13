namespace WorkTimeTracker.Domain.Entities
{
    public class DailyWorkSchedule
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime PlannedWorkStart { get; set; }
        public DateTime PlannedWorkEnd { get; set; }
        public TimeSpan WorkTimeNorm { get; set; }
        public TimeSpan BreakTimeNorm { get; set; }
        public DateTime RealWorkStart { get; set; }
        public DateTime RealWorkEnd { get; set; }
        public TimeSpan WorkHours { get; set; }
        public TimeSpan NightWorkHours { get; set; }
        public TimeSpan Overrime { get; set; }
        public TimeSpan NightOvertime { get; set; }
        public TimeSpan OvertimeCollected { get; set; }

        public List<WorkActionTime>? WorkActions { get; set; }
        public List<BreakActionTime>? BreakActions { get; set; }
        public Employee? Employee { get; set; }
    }
}
