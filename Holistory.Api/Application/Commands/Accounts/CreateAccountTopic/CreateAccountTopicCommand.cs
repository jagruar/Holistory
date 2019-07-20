using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Accounts.CreateAccountTopic
{
    [DataContract]
    public class CreateAccountTopicCommand : IRequest<int>
    {
        [DataMember]
        public int? AccountId { get; private set; }

        [DataMember]
        public int? TopicId { get; private set; }

        [DataMember]
        public int? Correct { get; private set; }

        [DataMember]
        public int? Incorrect { get; private set; }
    }
}
