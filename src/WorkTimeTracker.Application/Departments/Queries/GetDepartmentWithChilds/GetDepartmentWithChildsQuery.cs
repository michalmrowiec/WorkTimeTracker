using MediatR;

namespace WorkTimeTracker.Application.Departments.Queries.GetDepartmentWithChilds
{
    public record GetDepartmentWithChildsQuery(string DepartmentId) : IRequest<IEnumerable<DepartmentDetailsDto>>;
}
