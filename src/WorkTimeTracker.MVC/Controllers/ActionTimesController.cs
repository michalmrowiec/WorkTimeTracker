using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.ActionTimes.Commands.CreateActionTime;
using WorkTimeTracker.Application.ActionTimes.Commands.DeleteActionTime;
using WorkTimeTracker.Application.ActionTimes.Commands.UpdateActionTime;
using WorkTimeTracker.Application.ActionTimes.Queries.GetActionTimeById;
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
            var actinos = _context.ActionTimes
                .Include(x => x.Employee)
                .AsNoTracking()
                .ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePartial(CreateActionTimeCommand actionTime)
        {
            if (ModelState.IsValid || actionTime.Start > actionTime.End)
            {
                await _mediator.Send(actionTime);
                return actionTime.BackLink == null ?
                    RedirectToAction(nameof(Index))
                    : Redirect(actionTime.BackLink);
            }
            else
            {
                return View(actionTime);
            }
        }

        // GET: ActionTimesController/Edit/5
        public async Task<ActionResult> Edit(string id, string? backLink)
        {
            var actionTime = await _mediator.Send(new GetActionTimeByIdQuery(id));

            if (actionTime == null)
            {
                return NotFound();
            }

            return View(new UpdateActionTimeCommand()
            {
                Id = actionTime.Id,
                BackLink = backLink,
                EmployeeId = actionTime.EmployeeId,
                Start = actionTime.Start,
                End = actionTime.End,
                IsWork = actionTime.IsWork,
                TimeOfAction = actionTime.TimeOfAction
            });
        }

        // POST: ActionTimesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateActionTimeCommand updateActionTimeCommand)
        {
            if (ModelState.IsValid || updateActionTimeCommand.Start > updateActionTimeCommand.End)
            {
                await _mediator.Send(updateActionTimeCommand);

                return updateActionTimeCommand.BackLink == null ?
                    RedirectToAction(nameof(Index))
                    : Redirect(updateActionTimeCommand.BackLink);
            }
            else
            {
                return View(updateActionTimeCommand);
            }
        }

        // GET: ActionTimesController/Delete/5
        public async Task<ActionResult> Delete(string id, string? backLink)
        {
            var actionTime = await _mediator.Send(new GetActionTimeByIdQuery(id));

            if (actionTime == null)
            {
                return NotFound();
            }

            return View(new DeleteActionTimeCommand() { Id = actionTime.Id, BackLink = backLink });
        }

        // POST: ActionTimesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteActionTimeCommand actionTimeToDelete, IFormCollection collection)
        {
            try
            {
                await _mediator.Send(actionTimeToDelete);

                return actionTimeToDelete.BackLink == null ?
                    RedirectToAction(nameof(Index))
                    : Redirect(actionTimeToDelete.BackLink);
            }
            catch
            {
                return View();
            }
        }
    }
}
