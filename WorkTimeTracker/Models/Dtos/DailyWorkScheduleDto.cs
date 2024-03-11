using System.ComponentModel.DataAnnotations;
using WorkTimeTracker.Models.Entities;

namespace WorkTimeTracker.Models.Dtos
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

        public Employee? Employee { get; set; }
    }
}
