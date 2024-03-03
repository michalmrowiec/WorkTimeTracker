using Microsoft.AspNetCore.Identity;

namespace WorkTimeTracker.Models.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel {  get; set; }
        public string ReportsToId { get; set; }
        public double Workload { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string BadgeId { get; set; }

        public List<DailyWorkSchedule>? DailyWorkSchedules { get; set; }
    }
}
