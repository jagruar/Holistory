using Holistory.Domain.Aggregates.TopicAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Topics.CreateTopic
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, int>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateTopicCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<int> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            Topic topic = new Topic(
                request.Title,
                request.Description,
                request.StartDate.Value,
                request.EndDate.Value,
                request.Map,
                request.RegionId.Value,
                request.EraId.Value);

            await _topicRepository.AddAsync(topic);

            await _topicRepository.UnitOfWork.SaveEntitiesAsync();

            return topic.Id;
        }
    }
}
