using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.TopicAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Topics.CreateAnswer
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, int>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateAnswerCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<int> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            Topic topic = await _topicRepository.GetAsync(request.TopicId.Value);

            NotFoundException.ThrowIfNull(topic, nameof(topic));

            Answer answer = topic.AddAnswer(request.QuestionId.Value, request.Text, request.IsCorrect.Value);

            await _topicRepository.UnitOfWork.SaveEntitiesAsync();

            return answer.Id;
        }
    }
}
