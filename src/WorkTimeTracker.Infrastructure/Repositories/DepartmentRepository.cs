using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateDepartmentAsync(Department department)
        {
            await _context.Departments
                .AddAsync(department);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllChildDepartments(string departmentId)
        {
            List<Department> entireDepartments = new();

            var departments = await _context.Departments
                .Where(d => d.ParentDepartmentId == departmentId)
                .AsNoTracking()
                .ToListAsync();

            entireDepartments.AddRange(departments);

            foreach (var department in departments)
            {
                entireDepartments.AddRange(await GetAllChildDepartments(department.Id));
            }

            return entireDepartments;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments
                .Include(d => d.ParentDepartment)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
