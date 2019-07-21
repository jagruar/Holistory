using FluentValidation;

namespace Holistory.Api.Application.Commands.Portal.Users.GenerateAuthTokenCommand
{
    public class GenerateAuthTokenCommandValidator : AbstractValidator<GenerateAuthTokenCommand>
    {
        public GenerateAuthTokenCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
