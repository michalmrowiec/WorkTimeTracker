using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.ActionTimes.Commands;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;

namespace WorkTimeTracker.MVC.Controllers
{
    public class ActionTimesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public ActionTimesController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        // GET: ActionTimesController
        public ActionResult Index()
        {
            var actinos = _context.ActionTimes.ToList();
            return View(actinos);
        }

        // GET: ActionTimesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActionTimesController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Employees = await _context.Employees.
                Include(e => e.Department)
                .AsNoTracking()
                .ToListAsync();
            return View();
        }

        // POST: ActionTimesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateActionTimeCommand actionTime)
        {
            if (ModelState.IsValid || actionTime.Start > actionTime.End)
            {
                await _mediator.Send(actionTime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(actionTime);

            }
        }

        // GET: ActionTimesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActionTimesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ActionTime actionTime)
        {
            try
            {
                _context.ActionTimes.Add(actionTime);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(actionTime);
            }
        }

        // GET: ActionTimesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActionTimesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
