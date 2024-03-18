using MediatR;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails
{
    public record GetEmployeeDetailsQuery(string EmployeeId) : IRequest<EmployeeDetailsDto>;
}
