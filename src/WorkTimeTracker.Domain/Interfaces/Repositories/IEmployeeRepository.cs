using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeDetailsAsync(string employeeId);
    }
}
