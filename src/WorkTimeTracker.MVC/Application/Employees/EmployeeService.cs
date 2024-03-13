using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees
{
    public class EmployeeService
    {
        public readonly UserManager<Employee> _employeeManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeService(UserManager<Employee> employeeManager, RoleManager<IdentityRole> roleManager)
        {
            _employeeManager = employeeManager;
            _roleManager = roleManager;
        }

        public async Task<Employee> AddEmployee(Employee employee, string roleId)
        {
            await _employeeManager.CreateAsync(employee);
            await _employeeManager.AddToRoleAsync(employee, roleId);

            return new();
        }
    }
}
