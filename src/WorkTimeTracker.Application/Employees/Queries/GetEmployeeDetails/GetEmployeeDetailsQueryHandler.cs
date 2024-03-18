using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails
{
    internal class GetEmployeeDetailsQueryHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDto>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeDetailsQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeDetails(request.EmployeeId);


            if (employee == null)
            {
                return new();
            }

            var dto = _mapper.Map<EmployeeDetailsDto>(employee);

            return dto;
        }
    }
}
