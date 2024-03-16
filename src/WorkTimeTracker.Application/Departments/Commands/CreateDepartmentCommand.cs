using MediatR;

namespace WorkTimeTracker.Application.Departments.Commands
{
    public class CreateDepartmentCommand : DepartmentDto, IRequest
    {
    }
}
