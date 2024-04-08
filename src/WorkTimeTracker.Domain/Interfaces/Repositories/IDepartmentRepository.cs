using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task CreateDepartmentAsync(Department department);
        Task<IEnumerable<Department>> GetAllChildDepartments(string departmentId);
        Task<IEnumerable<Department>> GetDepartmentWithChilds(string departmentId);
    }
}
