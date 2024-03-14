using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
    }
}
