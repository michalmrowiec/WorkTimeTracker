using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task CreateDepartmentAsync(Department department);
    }
}
