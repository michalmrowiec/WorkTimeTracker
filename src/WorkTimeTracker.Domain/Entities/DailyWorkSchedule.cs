using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkTimeTracker.Domain.Entities
{
    public class DailyWorkSchedule
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        public TypeOfDay TypeOfDay { get; set; }
        public DateTime PlannedWorkStart { get; set; }
        public DateTime PlannedWorkEnd { get; set; }
        public TimeSpan PlannedWorkTime { get; set; }
        public TimeSpan PlannedBreakTime { get; set; }
        public DateTime? RealWorkStart { get; set; }
        public DateTime? RealWorkEnd { get; set; }
        public TimeSpan RealWorkTime { get; set; }
        public TimeSpan RealBreakTime { get; set; }
        public double RealOvertimeMinutes { get; set; }

        public List<ActionTime>? ActionTimes { get; set; }
        public Employee? Employee { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeOfDay
    {
        WorkDay,
        NightShift,
        BusinessTrip,
        RemoteWork,
        Vacation,
        OnDemandVacation,
        ChildCare,
        DayOff,
        MedicalLeave,
        Holiday,
        MaternityLeave,
        Unplanned
    }
}
