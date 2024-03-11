using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WorkTimeTracker.Data;
using WorkTimeTracker.Models.Dtos;
using WorkTimeTracker.Models.Entities;

namespace WorkTimeTracker.Controllers
{
    public class DailyWorkSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyWorkSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyWorkSchedules
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.DailyWorkSchedules.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? year, int? month)
        {
            if (year.HasValue && month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            //var schedule = await _context.Employees
            //    .ToDictionaryAsync(
            //        employee => employee,
            //        employee => _context.DailyWorkSchedules
            //            .Where(schedule => schedule.EmployeeId == employee.Id && schedule.Date.Month == month && schedule.Date.Year == year)
            //            .ToListAsync()
            //    );

            var employees = await _context.Employees.ToListAsync();
            var schedules = new Dictionary<Employee, List<DailyWorkSchedule>>();

            foreach (var employee in employees)
            {
                schedules[employee] = await _context.DailyWorkSchedules
                    .Where(schedule => schedule.EmployeeId == employee.Id && schedule.Date.Month == month && schedule.Date.Year == year)
                    .OrderBy(schedule => schedule.Date)
                    .ToListAsync();
            }

            //var schedule = await _context.DailyWorkSchedules
            //    .Include(dws => dws.Employee)
            //    .Where(s => s.Date.Year == year && s.Date.Month == month)
            //    .ToListAsync();
            return View(schedules);
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
            dailyWorkSchedule.Employee = await _context.Employees.FindAsync(employeeId);
            return View(dailyWorkSchedule);
        }

        // POST: DailyWorkSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        ///[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("EmployeeId,Date,PlannedWorkStart,PlannedWorkEnd")] DailyWorkScheduleDto dailyWorkSchedule)
        {
            if (ModelState.IsValid)
            {
                var schedule = new DailyWorkSchedule
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = dailyWorkSchedule.EmployeeId,
                    Date = dailyWorkSchedule.Date,
                    PlannedWorkStart = dailyWorkSchedule.PlannedWorkStart,
                    PlannedWorkEnd = dailyWorkSchedule.PlannedWorkEnd
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
