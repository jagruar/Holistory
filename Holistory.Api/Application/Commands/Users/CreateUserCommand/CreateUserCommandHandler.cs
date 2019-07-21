using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _accountRepository;

        public CreateUserCommandHandler(UserManager<User> userManager, IUserRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User existingUser = await _userManager.FindByNameAsync(request.Username);

            if (existingUser != null)
            {
                throw new DataValidationException(nameof(request.Username), $"Username '{request.Username}' is already taken.");
            }

            await _userManager.CreateAsync(new User(request.Username, request.Email), request.Password);
            User userCreated = await _userManager.FindByNameAsync(request.Username);

            return userCreated.Id;
        }
    }
}
