using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetByDepartmentDailyWorkSchedules;
using WorkTimeTracker.Application.Departments.Queries.GetDepartmentWithChilds;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;

namespace WorkTimeTracker.Controllers
{
    [Authorize]
    public class DailyWorkSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DailyWorkSchedulesController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int? year, int? month, string? departmentId)
        {
            var employeeDepartment = (await _mediator.Send(
                new GetEmployeeDetailsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)))).Department;

            if (year.HasValue == false || month.HasValue == false || departmentId == null)
            {
                if (HttpContext.Session.GetInt32("Year") != null
                && HttpContext.Session.GetInt32("Month") != null
                && HttpContext.Session.GetString("DepartmentId") != string.Empty)
                {
                    year = HttpContext.Session.GetInt32("Year");
                    month = HttpContext.Session.GetInt32("Month");
                    departmentId = HttpContext.Session.GetString("DepartmentId");
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                    departmentId = employeeDepartment.Id;
                }
            }

            HttpContext.Session.SetInt32("Year", year ?? 0);
            HttpContext.Session.SetInt32("Month", month ?? 0);
            HttpContext.Session.SetString("DepartmentId", departmentId ?? string.Empty);

            var schedules = await _mediator.Send(new GetByDepartmentDailyWorkSchedulesQuery(departmentId, (int)year, (int)month));

            Dictionary<EmployeeDto, List<DailyWorkScheduleDto>> res = schedules.ToDictionary(k => k.Key, k => k.Value as List<DailyWorkScheduleDto>);

            TempData["DateYear"] = year.ToString();
            TempData["DateMonth"] = month.ToString();

            var employeeDepartments = await _mediator.Send(new GetDepartmentWithChildsQuery(employeeDepartment.Id));

            ViewBag.Departments = employeeDepartments;
            TempData["EmployeeDepartmentId"] = departmentId.ToString();

            return View(res);
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
        public async Task<IActionResult> Create(string employeeId, string date)
        {
            var dailyWorkSchedule = new DailyWorkScheduleDto { EmployeeId = employeeId, Date = DateTime.Parse(date) };
            var empl = await _context.Employees.FindAsync(employeeId);
            dailyWorkSchedule.Employee = new EmployeeDto { Id = empl.Id, FirstName = empl.FirstName, LastName = empl.LastName };
            return View(dailyWorkSchedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("EmployeeId,Date,PlannedWorkStart,PlannedWorkEnd,WorkTimeNorm")] DailyWorkScheduleDto dailyWorkSchedule)
        {
            if (ModelState.IsValid)
            {
                var schedule = new DailyWorkSchedule
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = dailyWorkSchedule.EmployeeId,
                    Date = dailyWorkSchedule.PlannedWorkStart.Date,
                    PlannedWorkStart = dailyWorkSchedule.PlannedWorkStart,
                    PlannedWorkEnd = dailyWorkSchedule.PlannedWorkEnd,
                    WorkTimeNorm = dailyWorkSchedule.WorkTimeNorm
                };
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyWorkSchedule);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyWorkSchedule = await _context.DailyWorkSchedules.FindAsync(id);
            if (dailyWorkSchedule == null)
            {
                return NotFound();
            }
            return View(dailyWorkSchedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Id,Date,PlannedWorkStart,PlannedWorkEnd,WorkTimeNorm,BreakTimeNorm,RealWorkStart,RealWorkEnd,WorkHours,NightWorkHours,Overrime,NightOvertime,OvertimeCollected")] DailyWorkSchedule dailyWorkSchedule)
        {
            if (id != dailyWorkSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyWorkSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyWorkScheduleExists(dailyWorkSchedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dailyWorkSchedule);
        }

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
