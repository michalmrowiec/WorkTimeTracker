using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees.Commands.RegisterEmployee
{
    internal class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterEmployeeCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
                (firstName: request.FirstName,
                lastName: request.LastName,
                pesel: "",
                departmentId: request.DepartmentId,
                workload: request.Workload,
                dateOfEmployment: DateTime.Now,
                contractEndDate: null,
                badgeId: null);

            await _userManager.SetEmailAsync(employee, request.Email);
            await _userManager.SetUserNameAsync(employee, request.Email);

            var createUserResult = await _userManager.CreateAsync(employee, request.Password);

            if (createUserResult.Succeeded)
            {
                var addRolesResult = await _userManager.AddToRolesAsync(employee, request.Roles);
            }
        }
    }
}
