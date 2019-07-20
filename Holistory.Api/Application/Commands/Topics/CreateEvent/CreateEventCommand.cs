using MediatR;
using System;
using System.Runtime.Serialization;

namespace Holistory.Api.Application.Commands.Topics.CreateEvent
{
    [DataContract]
    public class CreateEventCommand : IRequest<int>
    {
        [DataMember]
        public int? TopicId { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Content { get; private set; }

        [DataMember]
        public DateTime? StartDate { get; private set; }

        [DataMember]
        public DateTime? EndDate { get; private set; }

        [DataMember]
        public int? X { get; private set; }

        [DataMember]
        public int? Y { get; private set; }

        [DataMember]
        public int? EventTypeId { get; private set; }
    }
}
