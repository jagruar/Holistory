using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Accounts.CreateAttempt
{
    public class CreateAttemptCommandHandler : IRequestHandler<CreateAttemptCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateAttemptCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateAttemptCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetAsync(request.UserId);
            NotFoundException.ThrowIfNull(user, nameof(user));

            Attempt attempt = user.AddAttempt(request.TopicId.Value, request.Correct.Value, request.Incorrect.Value);

            await _userRepository.UnitOfWork.SaveEntitiesAsync();

            return attempt.Id;
        }
    }
}
