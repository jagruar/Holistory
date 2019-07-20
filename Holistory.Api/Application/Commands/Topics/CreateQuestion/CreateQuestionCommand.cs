using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateQuestion
{
    [DataContract]
    public class CreateQuestionCommand : IRequest<int>
    {
        [DataMember]
        public int? TopicId { get; private set; }

        [DataMember]
        public int? EventId { get; private set; }

        [DataMember]
        public string Text { get; private set; }
    }
}
