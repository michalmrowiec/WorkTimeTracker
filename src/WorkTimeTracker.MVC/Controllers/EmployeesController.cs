﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.Departments.Queries;
using WorkTimeTracker.Application.Departments.Queries.GetAllDepartment;
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
            var result = await _mediator.Send(new GetEmployeesQuery());

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _mediator.Send(new GetAllDepartmentQuery());
            ViewBag.Departments = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,ConfirmPassword,Roles,DepartmentId")] RegisterEmployeeCommand employeeModel)
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
