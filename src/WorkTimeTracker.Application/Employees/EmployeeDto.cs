using System.ComponentModel.DataAnnotations;
using WorkTimeTracker.Application.Departments;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        [Display(Name = "Fist name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
        [Display(Name = "Department")]
        public string Department { get; set; }
    }
}
