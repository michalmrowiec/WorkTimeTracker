using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Application.Departments.Queries
{
    public class DepartmentDetailsDto : DepartmentDto
    {
        [Display(Name = "Parent department name")]
        public string? ParentDepartmentName { get; set; } = default!;
    }
}
