using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTimeTracker.Application.Departments;
using WorkTimeTracker.Application.Departments.Commands;
using WorkTimeTracker.Application.Departments.Queries.GetAllDepartment;

namespace WorkTimeTracker.MVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: DepartmentsController
        public async Task<IActionResult> Index()
        {
            var departments = await _mediator.Send(new GetAllDepartmentQuery());
            return View(departments);
        }

        // GET: DepartmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentCommand departmentDto)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(departmentDto);
            }
            else
            {
                return View(departmentDto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: DepartmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: DepartmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentsController/Delete/5
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
