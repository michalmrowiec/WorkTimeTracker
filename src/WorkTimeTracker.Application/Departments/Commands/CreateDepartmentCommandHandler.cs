using AutoMapper;
using MediatR;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces;

namespace WorkTimeTracker.Application.Departments.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            request.Id = Guid.NewGuid().ToString();
            var department = _mapper.Map<Department>(request);
            await _repository.CreateDepartmentAsync(department);
        }
    }
}
