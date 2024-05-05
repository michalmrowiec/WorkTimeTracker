using FluentValidation;
using MediatR;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;

namespace WorkTimeTracker.Application.Employees.Commands.RegisterEmployee
{
    internal class RegisterEmployeeValidator : AbstractValidator<RegisterEmployeeCommand>
    {
        private readonly IMediator _mediator;

        public RegisterEmployeeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .EmailAddress()
                .WithMessage("{PropertyName} must be email address")
                .Custom((value, context) =>
                {
                    var user = _mediator.Send(new GetEmployeeDetailsQuery(value)).Result;
                    if (user.Email != null)
                        context.AddFailure("Email", "Email is taken");
                });

            RuleFor(e => e.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
