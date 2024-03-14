using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.Employees.Commands.RegisterEmployee;
using WorkTimeTracker.Application.Employees.Queries.GetEmployees;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;

namespace WorkTimeTracker.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMediator _mediator;

        public EmployeesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMediator mediator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            //var employees = await _context.Employees.ToListAsync();
            //List<EmployeeDto> result = new List<EmployeeDto>();

            //foreach (var employee in employees)
            //{
            //    var roles = await _userManager.GetRolesAsync(employee);

            //    var emplDto = new EmployeeDto
            //    {
            //        Id = employee.Id,
            //        FirstName = employee.FirstName,
            //        LastName = employee.LastName,
            //        Roles = roles.ToList()
            //    };

            //    result.Add(emplDto);
            //}

            var result = await _mediator.Send(new GetEmployeesQuery());
            
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            List<Employee> managersNames = await _context.Employees.Where(e => managers.Select(e => e.Id).Contains(e.Id)).ToListAsync();
            ViewBag.Managers = managersNames;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,ConfirmPassword,Roles,ReportsToId")] RegisterEmployeeCommand employeeModel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(employeeModel);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

    }
}
