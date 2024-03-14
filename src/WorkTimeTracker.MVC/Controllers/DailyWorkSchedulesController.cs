using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkTimeTracker.Application.DailyWorkSchedules.Queries.GetDailyWorkSchedule;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;
using WorkTimeTracker.Application.Employees;

namespace WorkTimeTracker.Controllers
{
    public class DailyWorkSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DailyWorkSchedulesController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: DailyWorkSchedules
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.DailyWorkSchedules.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? year, int? month)
        {
            if (year.HasValue == false || month.HasValue == false)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            var schedules = await _mediator.Send(new GetDailyWorkScheduleQuery(User.FindFirstValue(ClaimTypes.NameIdentifier), (int)year, (int)month));
            //var employees = await _context.Employees.ToListAsync();
            //var schedules = new Dictionary<Employee, List<DailyWorkSchedule>>();

            //foreach (var employee in employees)
            //{
            //    schedules[employee] = await _context.DailyWorkSchedules
            //        .Where(schedule => schedule.EmployeeId == employee.Id && schedule.Date.Month == month && schedule.Date.Year == year)
            //        .OrderBy(schedule => schedule.Date)
            //        .ToListAsync();
            //}
            Dictionary<EmployeeDto, List<DailyWorkScheduleDto>> res = schedules.ToDictionary(k => k.Key, k => k.Value as List<DailyWorkScheduleDto>);
            TempData["DateYear"] = year.ToString();
            TempData["DateMonth"] = month.ToString();

            return View(res);
        }

        // GET: DailyWorkSchedules/Details/5
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

        // GET: DailyWorkSchedules/Create
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

        // POST: DailyWorkSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        ///[ValidateAntiForgeryToken]
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

        // GET: DailyWorkSchedules/Edit/5
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

        // POST: DailyWorkSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,EmployeeId,Date,PlannedWorkStart,PlannedWorkEnd,WorkTimeNorm,BreakTimeNorm,RealWorkStart,RealWorkEnd,WorkHours,NightWorkHours,Overrime,NightOvertime,OvertimeCollected")] DailyWorkSchedule dailyWorkSchedule)
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

        // GET: DailyWorkSchedules/Delete/5
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

        // POST: DailyWorkSchedules/Delete/5
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
