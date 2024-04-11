using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.Departments.Queries.GetDepartmentWithChilds
{
    internal class GetDepartmentWithChildsQueryHandler : IRequestHandler<GetDepartmentWithChildsQuery, IEnumerable<DepartmentDetailsDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetDepartmentWithChildsQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDetailsDto>> Handle(GetDepartmentWithChildsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repository.GetDepartmentWithChilds(request.DepartmentId);

            var mapped = _mapper.Map<List<DepartmentDetailsDto>>(departments);

            return mapped;
        }
    }
}
