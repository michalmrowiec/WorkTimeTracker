using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Security.Claims;
using WorkTimeTracker.Application.ApplicationUser;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.CreateDailyWorkSchedule;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.DailyWorkScheduleById;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules;
using WorkTimeTracker.Application.Departments.Queries;
using WorkTimeTracker.Application.Departments.Queries.GetAllDepartment;
using WorkTimeTracker.Application.Departments.Queries.GetDepartmentWithChilds;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Application.Employees.Queries.GetMonthlySummaryForEmployee;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;

namespace WorkTimeTracker.Controllers
{
    [Authorize]
    public class DailyWorkSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DailyWorkSchedulesController(ApplicationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? year, int? month, string? departmentId = null)
        {
            Dictionary<MonthlyScheduleEmployeeDto, List<DailyWorkScheduleDto>> schedules = new();
            List<DepartmentDetailsDto> availableDepartments = new();

            if (!year.HasValue || !month.HasValue)
            {
                if (HttpContext.Session.GetInt32("Year") != null
                && HttpContext.Session.GetInt32("Month") != null)
                {
                    year = HttpContext.Session.GetInt32("Year");
                    month = HttpContext.Session.GetInt32("Month");
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
            }

            HttpContext.Session.SetInt32("Year", year ?? 0);
            HttpContext.Session.SetInt32("Month", month ?? 0);

            TempData["DateYear"] = year.ToString();
            TempData["DateMonth"] = month.ToString();

            if (departmentId == null && HttpContext.Session.GetString("DepartmentId") != string.Empty)
            {
                departmentId = HttpContext.Session.GetString("DepartmentId");
            }

            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.Director.ToString())
                || User.IsInRole(Roles.HR.ToString()))
            {
                availableDepartments.AddRange(
                    await _mediator.Send(new GetAllDepartmentQuery()));

                ViewBag.Departments = availableDepartments;

                departmentId ??= availableDepartments.FirstOrDefault()?.Id ?? "";

                schedules.AddRange(
                    (await _mediator.Send(new GetMonthDailyWorkSchedulesByDepartmentQuery(departmentId, (int)year!, (int)month!)))
                    .ToDictionary(k => k.Key, v => (List<DailyWorkScheduleDto>)v.Value));
            }
            else
            {
                var employeeDepartment = (await _mediator.Send(
                        new GetEmployeeDetailsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""))).Department.Id ?? "";

                departmentId ??= employeeDepartment;

                schedules.AddRange(
                    (await _mediator.Send(new GetMonthDailyWorkSchedulesByDepartmentQuery(
                        departmentId, (int)year!, (int)month!)))
                    .ToDictionary(k => k.Key, v => (List<DailyWorkScheduleDto>)v.Value));

                availableDepartments.AddRange(
                    await _mediator.Send(new GetDepartmentWithChildsQuery(employeeDepartment)));

                ViewBag.Departments = availableDepartments;
            }

            HttpContext.Session.SetString("DepartmentId", departmentId ?? string.Empty);
            TempData["EmployeeDepartmentId"] = departmentId?.ToString();

            return View(schedules);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkSchedule = await _context.DailyWorkSchedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyWorkSchedule == null)
            {
                return NotFound();
            }

            return View(dailyWorkSchedule);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Director,HR,Manager,Admin")]
        public async Task<IActionResult> Create(string employeeId, string date)
        {
            //var dailyWorkSchedule = new DailyWorkScheduleDto { EmployeeId = employeeId, Date = DateTime.Parse(date) };
            //var empl = await _context.Employees.FindAsync(employeeId);
            //dailyWorkSchedule.Employee = new EmployeeDto { Id = empl.Id, FirstName = empl.FirstName, LastName = empl.LastName };

            var employee = await _mediator.Send(new GetEmployeeDetailsQuery(employeeId));

            var dailySchedule = new CreateDailyWorkScheduleCommand()
            {
                EmployeeId = employeeId,
                Employee = employee,
                Date = DateTime.Parse(date)
            };
            return View(dailySchedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Director,HR,Manager,Admin")]
        public async Task<IActionResult> Create(CreateDailyWorkScheduleCommand createDailyWorkSchedule)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(createDailyWorkSchedule);
                return RedirectToAction(nameof(Index));
            }
            return View(createDailyWorkSchedule);
        }

        [Authorize(Roles = "Director,HR,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkSchedule = await _mediator.Send(new DailyWorkScheduleByIdQuery(id));

            if (dailyWorkSchedule == null)
            {
                return NotFound();
            }

            return View(dailyWorkSchedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Director,HR,Manager,Admin")]
        public async Task<IActionResult> Edit(UpdateDailyWorkScheduleCommand updateDailyWorkScheduleCommand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(updateDailyWorkScheduleCommand);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(updateDailyWorkScheduleCommand);
        }

        [Authorize(Roles = "Director,HR,Manager,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkSchedule = await _context.DailyWorkSchedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyWorkSchedule == null)
            {
                return NotFound();
            }

            return View(dailyWorkSchedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Director,HR,Manager,Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dailyWorkSchedule = await _context.DailyWorkSchedules.FindAsync(id);
            if (dailyWorkSchedule != null)
            {
                _context.DailyWorkSchedules.Remove(dailyWorkSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyWorkScheduleExists(string id)
        {
            return _context.DailyWorkSchedules.Any(e => e.Id == id);
        }
    }
}
