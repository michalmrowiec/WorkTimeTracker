using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Application.Departments
{
    public class DepartmentDto
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string? ParentDepartmentId { get; set; }
    }
}
