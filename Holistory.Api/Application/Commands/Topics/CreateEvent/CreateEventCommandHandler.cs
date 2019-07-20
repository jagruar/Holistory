using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.TopicAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Topics.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
    {
        private readonly ITopicRepository _topicRepository;

        public CreateEventCommandHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            Topic topic = await _topicRepository.GetAsync(request.TopicId.Value);

            NotFoundException.ThrowIfNull(topic, nameof(topic));

            Event @event = topic.AddEvent(
                request.Title,
                request.Content,
                request.StartDate.Value,
                request.EndDate,
                request.X.Value,
                request.Y.Value,
                request.EventTypeId.Value);

            await _topicRepository.UnitOfWork.SaveEntitiesAsync();

            return @event.Id;
        }
    }
}
