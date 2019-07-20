using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.TopicAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Topics.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateQuestionCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<int> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            Topic topic = await _topicRepository.GetAsync(request.TopicId.Value);

            NotFoundException.ThrowIfNull(topic, nameof(topic));

            Question question = topic.AddQuestion(request.EventId, request.Text);

            await _topicRepository.UnitOfWork.SaveEntitiesAsync();

            return question.Id;
        }
    }
}
