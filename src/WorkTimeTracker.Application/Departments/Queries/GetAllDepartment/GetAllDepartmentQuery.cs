using MediatR;

namespace WorkTimeTracker.Application.Departments.Queries.GetAllDepartment
{
    public record GetAllDepartmentQuery : IRequest<IEnumerable<DepartmentDetailsDto>>;
}
