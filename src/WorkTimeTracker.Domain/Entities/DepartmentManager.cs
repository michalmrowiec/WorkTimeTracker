namespace WorkTimeTracker.Domain.Entities
{
    public class DepartmentManager
    {
        public string DepartmentId { get; set; } = default!;
        public Department? Department { get; set; }

        public string ManagerId { get; set; } = default!;
        public Employee? Manager { get; set; }
    }
}
