using WorkTimeTracker.Application.Departments;

namespace WorkTimeTracker.Application.Employees
{
    public class EmployeeDetailsDto : EmployeeDto
    {
        public DepartmentDto Department { get; set; }
    }
}
