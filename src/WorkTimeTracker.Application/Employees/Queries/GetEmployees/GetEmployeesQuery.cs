using MediatR;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {

    }
}
