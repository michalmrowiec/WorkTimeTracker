using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Data;
using WorkTimeTracker.Models.Dtos;
using WorkTimeTracker.Models.Entities;

namespace WorkTimeTracker.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                var roles = await _userManager.GetRolesAsync(employee);

                var emplDto = new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Roles = string.Join(", ", roles)
                };

                result.Add(emplDto);
            }
            
            return View(result);
        }
    }
}
