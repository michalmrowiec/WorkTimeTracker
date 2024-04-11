using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Application.Employees
{
    public class CreateEditEmployeeDto
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = default!;

        [Required]
        [Display(Name = "Roles")]
        public List<string> Roles { get; set; } = default!;

        [Required]
        [Display(Name = "Department")]
        public string? DepartmentId { get; set; }

        [Display(Name = "Workload")]
        [Range(0, 1)]
        public double Workload { get; set; }
    }
}
