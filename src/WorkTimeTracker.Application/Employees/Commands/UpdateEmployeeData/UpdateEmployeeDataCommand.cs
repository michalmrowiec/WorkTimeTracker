using MediatR;
using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Application.Employees.Commands.UpdateEmployeeData
{
    public class UpdateEmployeeDataCommand : CreateEditEmployeeDto, IRequest<EmployeeDetailsDto>
    {
        [Required]
        public string Id { get; set; } = default!;
    }
}
