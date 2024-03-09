using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Data;
using WorkTimeTracker.Models;
using WorkTimeTracker.Models.Dtos;
using WorkTimeTracker.Models.Entities;

namespace WorkTimeTracker.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Roles = roles.ToList()
                };

                result.Add(emplDto);
            }
            
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,ConfirmPassword,Roles")] CreateEmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee(employeeModel.FirstName, employeeModel.LastName, "", null, 0, DateTime.Now, null, null);
                await _userManager.SetEmailAsync(employee, employeeModel.Email);
                await _userManager.SetUserNameAsync(employee, employeeModel.Email);
                var res1 = await _userManager.CreateAsync(employee, employeeModel.Password);
                var res2 = await _userManager.AddToRolesAsync(employee, employeeModel.Roles);

                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

    }
}
