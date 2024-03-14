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
            var employee = new Employee(request.FirstName, request.LastName, "", request.ReportsToId, 0, DateTime.Now, null, null);
            await _userManager.SetEmailAsync(employee, request.Email);
            await _userManager.SetUserNameAsync(employee, request.Email);
            var res1 = await _userManager.CreateAsync(employee, request.Password);
            if (res1.Succeeded)
            {
                var res2 = await _userManager.AddToRolesAsync(employee, request.Roles);
            }
        }
    }
}
