using MediatR;

namespace WorkTimeTracker.Application.Employees.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommand : CreateEmployeeModel, IRequest
    {

    }
}
