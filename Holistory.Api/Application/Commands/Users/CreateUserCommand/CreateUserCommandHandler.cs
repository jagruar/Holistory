using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.AccountAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountRepository _accountRepository;

        public CreateUserCommandHandler(UserManager<User> userManager, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User existingUser = await _userManager.FindByNameAsync(request.Username);

            if (existingUser != null)
            {
                throw new DataValidationException(nameof(request.Username), $"Username '{request.Username}' is already taken.");
            }

            await _userManager.CreateAsync(new User(request.Username, request.Email), request.Password);
            User userCreated = await _userManager.FindByNameAsync(request.Username);

            Account account = await _accountRepository.AddAsync(new Account(userCreated.Id));

            await _accountRepository.UnitOfWork.SaveEntitiesAsync();

            return account.Id;
        }
    }
}
