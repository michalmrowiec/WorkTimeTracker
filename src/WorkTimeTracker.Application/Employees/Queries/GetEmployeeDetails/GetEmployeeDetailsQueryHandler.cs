using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails
{
    internal class GetEmployeeDetailsQueryHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDto>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetEmployeeDetailsQueryHandler(IEmployeeRepository repository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeDetailsAsync(request.EmployeeId);

            if (employee == null)
            {
                return new();
            }

            var dto = _mapper.Map<EmployeeDetailsDto>(employee);

            var employeeRoles = await _userManager.GetRolesAsync(employee);
            dto.Roles = employeeRoles.ToList();

            return dto;
        }
    }
}
