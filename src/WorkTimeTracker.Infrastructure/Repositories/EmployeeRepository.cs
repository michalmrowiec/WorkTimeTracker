using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeDetails(string employeeId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
