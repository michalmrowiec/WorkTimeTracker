using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Application.Employees.Queries.GetEmployees
{
    internal class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetEmployeesQueryHandler(IEmployeeRepository repository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetEmployeesAsync();
            var dtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            foreach (var d in dtos)
            {
                var roles = await _userManager.GetRolesAsync(employees.First(e => e.Id == d.Id));
                d.Roles = roles.ToList();
            }

            return dtos;
        }
    }
}
