using FluentValidation;

namespace Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Matches(x => x.Password).WithMessage($"{nameof(CreateUserCommand.Password)} and {nameof(CreateUserCommand.ConfirmPassword)} must match.");
        }
    }
}
