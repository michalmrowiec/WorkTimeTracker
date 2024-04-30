namespace WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee
{
    public class MonthlyScheduleEmployeeDto : EmployeeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public TimeSpan MonthlyHourNorm { get; set; }
        public TimeSpan SumOfPlannedWorkHours { get; set; }
        public TimeSpan SumOfRealWorkHours { get; set; }
        public double SumOfRealOvertimeMinutes { get; set; }
    }
}
