using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetMonthDailyWorkSchedulesByDepartment
{
    public class MonthlyScheduleEmployeeDto : EmployeeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public TimeSpan MonthlyHourNorm { get; set; }
        public TimeSpan SumOfPlannedWorkHours { get; set; }
        public TimeSpan SumOfRealWorkHours { get; set; }
        public TimeSpan SumOfNightWorkHours { get; set; }
        public TimeSpan SumOfOvertime { get; set; }
        public TimeSpan SumOfNightOvertime { get; set; }
        public TimeSpan SumOfOvertimeCollected { get; set; }
    }
}
