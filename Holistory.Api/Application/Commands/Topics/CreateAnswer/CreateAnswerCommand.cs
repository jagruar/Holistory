using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateAnswer
{
    [DataContract]
    public class CreateAnswerCommand : IRequest<int>
    {
        [DataMember]
        public int? TopicId { get; private set; }

        [DataMember]
        public int? QuestionId { get; private set; }

        [DataMember]
        public string Text { get; private set; }

        [DataMember]
        public bool? IsCorrect { get; private set; }
    }
}
