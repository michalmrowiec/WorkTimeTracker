using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees.Commands.RegisterEmployee
{
    internal class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, AppResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public RegisterEmployeeCommandHandler(UserManager<IdentityUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<AppResponse> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterEmployeeValidator(_mediator);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new AppResponse(validationResult);
            }

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

            return new AppResponse();
        }
    }
}
