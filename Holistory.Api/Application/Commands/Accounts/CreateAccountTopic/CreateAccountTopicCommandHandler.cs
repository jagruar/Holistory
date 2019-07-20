using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.AccountAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Accounts.CreateAccountTopic
{
    public class CreateAccountTopicCommandHandler : IRequestHandler<CreateAccountTopicCommand, int>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountTopicCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> Handle(CreateAccountTopicCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(request.AccountId.Value);
            NotFoundException.ThrowIfNull(account, nameof(account));

            AccountTopic accountTopic = account.AddTopic(request.TopicId.Value, request.Correct.Value, request.Incorrect.Value);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync();

            return accountTopic.Id;
        }
    }
}
