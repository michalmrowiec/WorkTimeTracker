namespace WorkTimeTracker.Domain.Entities
{
    public class Department
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public string? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }

        public List<Employee> Employees { get; set; } = new();
        public List<DepartmentManager>? Managers { get; set; }
    }
}
