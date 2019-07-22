using Holistory.Api.DataTranserObjects;
using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateAttempt
{
    [DataContract]
    public class CreateAttemptCommand : IRequest<AttemptDto>
    {
        [DataMember]
        public string UserId { get; private set; }

        [DataMember]
        public int? TopicId { get; private set; }

        [DataMember]
        public int? Correct { get; private set; }

        [DataMember]
        public int? Incorrect { get; private set; }
    }
}
