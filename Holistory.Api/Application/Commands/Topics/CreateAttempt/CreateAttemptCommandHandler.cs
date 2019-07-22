using AutoMapper;
using Holistory.Api.DataTranserObjects;
using Holistory.Common.Exceptions;
using Holistory.Domain.Aggregates.TopicAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Api.Application.Commands.Topics.CreateAttempt
{
    public class CreateAttemptCommandHandler : IRequestHandler<CreateAttemptCommand, AttemptDto>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public CreateAttemptCommandHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<AttemptDto> Handle(CreateAttemptCommand request, CancellationToken cancellationToken)
        {
            Topic topic = await _topicRepository.GetAsync(request.TopicId.Value);
            NotFoundException.ThrowIfNull(topic, nameof(topic));

            Attempt attempt = topic.AddAttempt(request.UserId, request.Correct.Value, request.Incorrect.Value);

            await _topicRepository.UnitOfWork.SaveEntitiesAsync();

            return _mapper.Map<AttemptDto>(attempt);
        }
    }
}
