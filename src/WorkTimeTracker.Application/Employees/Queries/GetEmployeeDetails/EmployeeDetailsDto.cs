using WorkTimeTracker.Application.Departments;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails
{
    public class EmployeeDetailsDto : EmployeeDto
    {
        public DepartmentDto Department { get; set; }
    }
}
