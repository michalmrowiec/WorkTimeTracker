using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees
{
    public class EmployeeControler : Controller
    {
        [Authorize(Roles = "HR,Dyrektor,Administrator")]
        public IActionResult AddEmployee(Employee employee)
        {

            return View(employee);
        }
    }
}
