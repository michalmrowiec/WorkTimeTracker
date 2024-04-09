using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Employees.Commands.UpdateEmployeeData
{
    internal class UpdateEmployeeDataCommandHandler : IRequestHandler<UpdateEmployeeDataCommand, EmployeeDetailsDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public UpdateEmployeeDataCommandHandler(UserManager<IdentityUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<EmployeeDetailsDto> Handle(UpdateEmployeeDataCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id);

            var employee = oldUser is Employee ? (Employee)oldUser : null;

            if (employee == null)
            {
                return new();
            }

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.DepartmentId = request.DepartmentId;
            employee.Workload = request.Workload;

            var currentRoles = await _userManager.GetRolesAsync(employee);

            var rolesToAdd = request.Roles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(request.Roles);

            await _userManager.UpdateAsync(employee);
            await _userManager.RemoveFromRolesAsync(employee, rolesToRemove);
            await _userManager.AddToRolesAsync(employee, rolesToAdd);

            return await _mediator.Send(new GetEmployeeDetailsQuery(employee.Id), cancellationToken);
        }
    }
}
