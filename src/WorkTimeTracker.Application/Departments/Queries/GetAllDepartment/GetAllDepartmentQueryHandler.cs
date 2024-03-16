using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Application.Departments.Queries.GetAllDepartment
{
    internal class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repository.GetDepartmentsAsync();

            var mapped = _mapper.Map<List<DepartmentDto>>(departments);

            return mapped;
        }
    }
}
