using MediatR;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Accounts.CreateAttempt
{
    [DataContract]
    public class CreateAttemptCommand : IRequest<int>
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
